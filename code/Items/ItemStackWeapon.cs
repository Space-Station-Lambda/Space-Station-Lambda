using System.Collections.Generic;
using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public partial class ItemStackWeapon : ItemStack
    {
        public virtual float PrimaryRate => 5.0f;
        public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
        
        public ItemStackWeapon()
        {
        }

        public ItemStackWeapon(Item item, int amount = 1) : base(item, amount)
        {
        }
        
        [Net, Predicted] private TimeSince TimeSincePrimaryAttack { get; set; }

        public override void Spawn()
        {
            base.Spawn();

            CollisionGroup = CollisionGroup.Weapon; // so players touch it as a trigger but not as a solid
            SetInteractsAs(CollisionLayer.Debris); // so player movement doesn't walk into it
        }

        public override void Simulate(Client player)
        {
            if (CanReload())
            {
                Reload();
            }
            
            if (CanPrimaryAttack())
            {
                TimeSincePrimaryAttack = 0;
                AttackPrimary();
            }
        }

        protected virtual bool CanReload()
        {
            return Owner.IsValid() && Input.Down(InputButton.Reload);
        }

        protected virtual void Reload()
        {
        }

        public virtual bool CanPrimaryAttack()
        {
            if (!Owner.IsValid() || !Input.Down(InputButton.Attack1)) return false;
            
            float rate = PrimaryRate;
            if (rate <= 0) return true;

            return TimeSincePrimaryAttack > (1 / rate);
        }

        public virtual void AttackPrimary()
        {
            TimeSincePrimaryAttack = 0;

            using (Prediction.Off())
            {
                ShootEffects();
            }

            //TODO: Bullet to class
            ShootBullet(0.05f, 1.5f, 1, 3.0f);
        }

        /// <summary>
        /// Does a trace from start to end, does bullet impact effects. Coded as an IEnumerable so you can return multiple
        /// hits, like if you're going through layers or ricocet'ing or something.
        /// </summary>
        public virtual TraceResult TraceBullet(Vector3 start, Vector3 end, float radius = 2.0f)
        {
            bool inWater = Physics.TestPointContents(start, CollisionLayer.Water);

            TraceResult tr = Trace.Ray(start, end)
                .UseHitboxes()
                .HitLayer(CollisionLayer.Water, !inWater)
                .Ignore(Owner)
                .Ignore(this)
                .Size(radius)
                .Run();
            
            return tr;

            //
            // Another trace, bullet going through thin material, penetrating water surface?
            //
        }
        public virtual void ShootBullet(float spread, float force, float damage, float bulletSize)
        {
            Vector3 forward = Owner.EyeRot.Forward;
            forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
            forward = forward.Normal;

            TraceResult tr = TraceBullet(Owner.EyePos, Owner.EyePos + forward * 5000, bulletSize);
            
            if (!IsServer || !tr.Entity.IsValid()) return;
            
            tr.Surface.DoBulletImpact(tr);
            
            DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPos, forward * 100 * force, damage)
                .UsingTraceResult(tr)
                .WithAttacker(Owner)
                .WithWeapon(this);

            tr.Entity.TakeDamage(damageInfo);
        }
        
        [ClientRpc]
        protected virtual void ShootEffects()
        {
            Host.AssertClient();
            
            Particles.Create("particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle");
          

            if (IsLocalPawn)
            {
                new Sandbox.ScreenShake.Perlin();
            }

            ViewModelEntity?.SetAnimBool("fire", true);
            CrosshairPanel?.CreateEvent("fire");
        }
    }
}
