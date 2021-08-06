using System.Runtime.CompilerServices;
using Sandbox;
using ssl.Items.Data;
using ssl.Player;

namespace ssl.Items.Carriables
{
    public partial class ItemWeapon : Item
    {
        public ItemWeapon()
        {
            
        }
        public ItemWeapon(ItemWeaponData weaponData) : base(weaponData)
        {
            PrimaryRate = weaponData.PrimaryRate;
        }

        [Net] public float PrimaryRate { get; private set; }
        [Net, Predicted] public TimeSince TimeSincePrimaryAttack { get; set; }
        
        public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
        
        public override void UsedOn(MainPlayer player)
        {
            base.UsedOn(player);

            if (CanPrimaryAttack())
            {
                AttackPrimary();
            }
        }

        protected bool CanReload()
        {
            return Owner.IsValid() && Input.Down(InputButton.Reload);
        }

        protected void Reload()
        {
        }

        private bool CanPrimaryAttack()
        {
            if (!Owner.IsValid()) return false;
            if (PrimaryRate <= 0) return true;

            return TimeSincePrimaryAttack > (1 / PrimaryRate);
        }

        protected void AttackPrimary()
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
        protected virtual TraceResult TraceBullet(Vector3 start, Vector3 end, float radius = 2.0f)
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

        [ClientRpc]
        private void ShootEffects()
        {
            if (!Host.IsClient) return;
            
            Particles.Create("particles/pistol_muzzleflash.vpcf", this, "muzzle");
            
            if (!IsLocalPawn)
            {
                new Sandbox.ScreenShake.Perlin();
            }
            
            ViewModelEntity?.SetAnimBool("fire", true);
            CrosshairPanel?.CreateEvent("fire");
        }

        protected virtual void ShootBullet(float spread, float force, float damage, float bulletSize)
        {
            Vector3 forward = Owner.EyeRot.Forward;
            forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
            forward = forward.Normal;

            TraceResult tr = TraceBullet( Owner.EyePos, Owner.EyePos + forward * 5000, bulletSize);
            
            if (!IsServer || !tr.Entity.IsValid()) return;
            
            tr.Surface.DoBulletImpact(tr);
            
            DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPos, forward * 100 * force, damage)
                .UsingTraceResult(tr)
                .WithAttacker(Owner)
                .WithWeapon(this);

            tr.Entity.TakeDamage(damageInfo);
        }
    }
}