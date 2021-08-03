using Sandbox;
using ssl.Player;

namespace ssl.Items.Data.Weapon
{
    public partial class ItemWeapon : Item
    {
        private const float PrimaryRate = 5.0F;

        public ItemWeapon(string id, string name, string model) : base(id, name, model)
        {
        }

        public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

        public override void OnInit(ItemStack itemStack)
        {
            base.OnInit(itemStack);
            itemStack.Data = new WeaponData();
        }

        public override void OnSimulate(MainPlayer player, ItemStack itemStack)
        {
            base.OnSimulate(player, itemStack);
            if (itemStack.Data is not WeaponData data || !CanPrimaryAttack(itemStack)) return;
            data.TimeSincePrimaryAttack = 0;
            AttackPrimary(itemStack);
        }

        protected bool CanReload(ItemStack stack)
        {
            return stack.Owner.IsValid() && Input.Down(InputButton.Reload);
        }

        protected void Reload(ItemStack stack)
        {
        }

        protected bool CanPrimaryAttack(ItemStack stack)
        {
            WeaponData data = stack.Data as WeaponData;
            
            if (!stack.Owner.IsValid() || !Input.Down(InputButton.Attack1) || data == null) return false;
            
            float rate = PrimaryRate;
            if (rate <= 0) return true;

            return data.TimeSincePrimaryAttack > (1 / rate);
        }

        protected void AttackPrimary(ItemStack stack)
        {
            Log.Trace("Attack");
            WeaponData data = stack.Data as WeaponData;

            data.TimeSincePrimaryAttack = 0;

            using (Prediction.Off())
            {
                ShootEffects(stack);
            }

            //TODO: Bullet to class
            ShootBullet(stack, 0.05f, 1.5f, 1, 3.0f);
        }

        /// <summary>
        /// Does a trace from start to end, does bullet impact effects. Coded as an IEnumerable so you can return multiple
        /// hits, like if you're going through layers or ricocet'ing or something.
        /// </summary>
        protected virtual TraceResult TraceBullet(ItemStack stack, Vector3 start, Vector3 end, float radius = 2.0f)
        {
            bool inWater = Physics.TestPointContents(start, CollisionLayer.Water);

            TraceResult tr = Trace.Ray(start, end)
                .UseHitboxes()
                .HitLayer(CollisionLayer.Water, !inWater)
                .Ignore(stack.Owner)
                .Ignore(stack)
                .Size(radius)
                .Run();
            
            return tr;

            //
            // Another trace, bullet going through thin material, penetrating water surface?
            //
        }
        public virtual void ShootBullet(ItemStack stack, float spread, float force, float damage, float bulletSize)
        {
            Vector3 forward = stack.Owner.EyeRot.Forward;
            forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
            forward = forward.Normal;

            TraceResult tr = TraceBullet(stack, stack.Owner.EyePos, stack.Owner.EyePos + forward * 5000, bulletSize);

            tr.Surface.DoBulletImpact(tr);
            
            if (!stack.IsServer || !tr.Entity.IsValid()) return;

            DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPos, forward * 100 * force, damage)
                .UsingTraceResult(tr)
                .WithAttacker(stack.Owner)
                .WithWeapon(stack);

            tr.Entity.TakeDamage(damageInfo);
        }
        
        protected static void ShootEffects(BaseCarriable entity)
        {
            Particles.Create("particles/pistol_muzzleflash.vpcf", entity, "muzzle");
            
            if (!entity.IsLocalPawn) return;
            
            new Sandbox.ScreenShake.Perlin();
            
            entity.ViewModelEntity?.SetAnimBool("fire", true);
            entity.CrosshairPanel?.CreateEvent("fire");
        }
    }
}