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
            SetInteractsExclude(CollisionLayer.Player);
        }
        
        public PlayerCorpse(Player player) : this()
        {
            Player = player;
        }

        [Net] public Player Player { get; private set; }

        public void CopyFrom(Player player)
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

        public void OnSelectStart(Player player)
        {
        }

        public void OnSelectStop(Player player)
        {
        }

        public void OnSelect(Player player)
        {
        }

        public void OnInteract(Player player)
        {
        }

        public void OnDragStart(Player player)
        {
        }

        public void OnDragStop(Player player)
        {
        }

        public void OnDrag(Player player)
        {
        }

        public bool IsDraggable(Player player)
        {
            return true;
        }
    }
}