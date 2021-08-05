using Sandbox;
using ssl.Player;

namespace ssl.Items.Data.Weapon
{
    public partial class ItemWeapon : Item
    {
        private const float PrimaryRate = 5.0F;

        public ItemWeapon(ItemWeaponData weaponData) : base(weaponData)
        {
        }

        public int TimeSincePrimaryAttack { get; set; }
        
        public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
        
        public override void UsedBy(MainPlayer player)
        {
            base.UsedBy(player);
            
            TimeSincePrimaryAttack = 0;
            AttackPrimary();
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
            if (!Owner.IsValid() || !Input.Down(InputButton.Attack1)) return false;
            
            float rate = PrimaryRate;
            if (rate <= 0) return true;

            return TimeSincePrimaryAttack > (1 / rate);
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
        protected virtual void ShootBullet(float spread, float force, float damage, float bulletSize)
        {
            Vector3 forward = Owner.EyeRot.Forward;
            forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
            forward = forward.Normal;

            TraceResult tr = TraceBullet( Owner.EyePos, Owner.EyePos + forward * 5000, bulletSize);

            tr.Surface.DoBulletImpact(tr);
            
            if (!IsServer || !tr.Entity.IsValid()) return;

            DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPos, forward * 100 * force, damage)
                .UsingTraceResult(tr)
                .WithAttacker(Owner)
                .WithWeapon(this);

            tr.Entity.TakeDamage(damageInfo);
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
    }
}