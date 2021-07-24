using System.Collections.Generic;
using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public partial class ItemStackWeapon : ItemStack
    {
        public virtual float PrimaryRate => 5.0f;
        public virtual float SecondaryRate => 15.0f;

        public ItemStackWeapon()
        {
        }

        public ItemStackWeapon(Item item, int amount = 1) : base(item, amount)
        {
        }

        public override void Spawn()
        {
            base.Spawn();

            CollisionGroup = CollisionGroup.Weapon; // so players touch it as a trigger but not as a solid
            SetInteractsAs(CollisionLayer.Debris); // so player movement doesn't walk into it
        }

        [Net, Predicted] public TimeSince TimeSincePrimaryAttack { get; set; }

        [Net, Predicted] public TimeSince TimeSinceSecondaryAttack { get; set; }

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
            
            if (CanSecondaryAttack())
            {
                TimeSinceSecondaryAttack = 0;
                AttackSecondary();
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
            
            Log.Info("Primary attack");
            
            float rate = PrimaryRate;
            if (rate <= 0) return true;

            return TimeSincePrimaryAttack > (1 / rate);
        }

        public virtual void AttackPrimary()
        {
        }

        public virtual bool CanSecondaryAttack()
        {
            if (!Owner.IsValid() || !Input.Down(InputButton.Attack2)) return false;

            float rate = SecondaryRate;
            if (rate <= 0) return true;

            return TimeSinceSecondaryAttack > (1 / rate);
        }

        protected virtual void AttackSecondary()
        {
        }

        /// <summary>
        /// Does a trace from start to end, does bullet impact effects. Coded as an IEnumerable so you can return multiple
        /// hits, like if you're going through layers or ricocet'ing or something.
        /// </summary>
        public virtual IEnumerable<TraceResult> TraceBullet(Vector3 start, Vector3 end, float radius = 2.0f)
        {
            bool inWater = Physics.TestPointContents(start, CollisionLayer.Water);

            TraceResult tr = Trace.Ray(start, end)
                .UseHitboxes()
                .HitLayer(CollisionLayer.Water, !inWater)
                .Ignore(Owner)
                .Ignore(this)
                .Size(radius)
                .Run();

            yield return tr;

            //
            // Another trace, bullet going through thin material, penetrating water surface?
            //
        }
    }
}