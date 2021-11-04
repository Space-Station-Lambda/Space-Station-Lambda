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
        
        public PlayerCorpse(SslPlayer sslPlayer) : this()
        {
            SslPlayer = sslPlayer;
        }

        [Net] public SslPlayer SslPlayer { get; private set; }

        public void CopyFrom(SslPlayer sslPlayer)
        {
            SetModel(sslPlayer.GetModelName());
            TakeDecalsFrom(sslPlayer);

            // We have to use `this` to refer to the extension methods.
            this.CopyBonesFrom(sslPlayer);
            this.SetRagdollVelocityFrom(sslPlayer);

            foreach (Entity child in sslPlayer.Children)
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

        public void OnSelectStart(SslPlayer sslPlayer)
        {
        }

        public void OnSelectStop(SslPlayer sslPlayer)
        {
        }

        public void OnSelect(SslPlayer sslPlayer)
        {
        }

        public void OnInteract(SslPlayer sslPlayer)
        {
        }

        public void OnDragStart(SslPlayer sslPlayer)
        {
        }

        public void OnDragStop(SslPlayer sslPlayer)
        {
        }

        public void OnDrag(SslPlayer sslPlayer)
        {
        }

        public bool IsDraggable(SslPlayer sslPlayer)
        {
            return true;
        }
    }
}