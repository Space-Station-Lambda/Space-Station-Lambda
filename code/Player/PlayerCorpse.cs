using Sandbox;
using ssl.Modules.Selection;

namespace ssl.Player
{
    public partial class PlayerCorpse : ModelEntity, IDraggable
    {
        private const float ForceMultiplier = 1000F;
        private const string ClothesModelIndicator = "clothes";

        public PlayerCorpse()
        {
            MoveType = MoveType.Physics;
            UsePhysicsCollision = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;
            
            SetInteractsAs(CollisionLayer.Hitbox | CollisionLayer.Debris);
            SetInteractsWith(CollisionLayer.WORLD_GEOMETRY);
            SetInteractsExclude(CollisionLayer.Player | CollisionLayer.Debris);
        }
        
        public PlayerCorpse(MainPlayer player) : this()
        {
            Player = player;
        }

        [Net] public MainPlayer Player { get; private set; }
        public PhysicsBody Body => PhysicsGroup.GetBody(0);

        public void CopyFrom(MainPlayer player)
        {
            SetModel(player.GetModelName());
            TakeDecalsFrom(player);

            // We have to use `this` to refer to the extension methods.
            this.CopyBonesFrom(player);
            this.SetRagdollVelocityFrom(player);

            foreach (Entity child in player.Children)
            {
                if (child is ModelEntity e)
                {
                    string model = e.GetModelName();

                    if (model != null && !model.Contains(ClothesModelIndicator))
                        continue;

                    ModelEntity clothing = new();
                    clothing.SetModel(model);
                    clothing.SetParent(this, true);
                    clothing.EnableHideInFirstPerson = true;
                }
            }
        }

        public void ApplyForceToBone(Vector3 force, int forceBone)
        {
            PhysicsGroup.AddVelocity(force);

            if (forceBone >= 0)
            {
                PhysicsBody body = GetBonePhysicsBody(forceBone);

                if (body != null)
                {
                    body.ApplyForce(force * ForceMultiplier);
                }
                else
                {
                    PhysicsGroup.AddVelocity(force);
                }
            }
        }

        public void OnSelectStart(MainPlayer player)
        {
        }

        public void OnSelectStop(MainPlayer player)
        {
        }

        public void OnSelect(MainPlayer player)
        {
        }

        public void OnInteract(MainPlayer player)
        {
        }

        public void OnDragStart(MainPlayer player)
        {
        }

        public void OnDragStop(MainPlayer player)
        {
        }

        public void OnDrag(MainPlayer player)
        {
        }

        public bool IsDraggable(MainPlayer player)
        {
            return true;
        }
    }
}