using Sandbox;
using Sandbox.ScreenShake;
using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemWeapon : Item
    {
        private const float MaxRange = 5000;

        public ItemWeapon()
        {
        }

        public ItemWeapon(ItemWeaponData weaponData) : base(weaponData)
        {
            PrimaryRate = weaponData.PrimaryRate;
            Range = weaponData.Range;
            Damage = weaponData.Damage;
        }

        /// <summary>
        /// PrimaryRate of the weapon
        /// </summary>
        [Net]
        public float PrimaryRate { get; private set; }

        /// <summary>
        /// Range of the weapon, 0 means @MaxRange
        /// </summary>
        [Net]
        public float Range { get; private set; }

        /// <summary>
        /// Damage of the weapon
        /// </summary>
        [Net]
        public float Damage { get; private set; }

        [Net, Predicted] public TimeSince TimeSincePrimaryAttack { get; set; }

        public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

        public override void UseOn(MainPlayer player)
        {
            base.UseOn(player);

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
            ShootBullet(0.05f, 1.5f, 3.0f);
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

            if (IsLocalPawn)
            {
                _ = new Perlin();
            }

            ViewModelEntity?.SetAnimBool("fire", true);
            CrosshairPanel?.CreateEvent("fire");
        }

        protected virtual void ShootBullet(float spread, float force, float bulletSize)
        {
            Vector3 forward = Owner.EyeRot.Forward;
            forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
            forward = forward.Normal;

            //If the range is 0, the range is max.
            float range = Range == 0 ? MaxRange : Range;
            TraceResult tr = TraceBullet(Owner.EyePos, Owner.EyePos + forward * range, bulletSize);

            if (!IsServer || !tr.Entity.IsValid()) return;

            tr.Surface.DoBulletImpact(tr);

            DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPos, forward * 100 * force, Damage)
                .UsingTraceResult(tr)
                .WithAttacker(Owner)
                .WithWeapon(this);

            tr.Entity.TakeDamage(damageInfo);
        }
    }
}