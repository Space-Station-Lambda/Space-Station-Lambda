using Sandbox;

namespace ssl.Player
{
    public partial class MainPlayer
    {
        private const int PositionVelocity = 40;
        private const int PhysicGroupVelocity = 5000;
        
        /// <summary>
        /// Called on respawn
        /// </summary>
        public override void Respawn()
        {
            SetModel(Model);

            Controller = new WalkController();
            Animator = new StandardPlayerAnimator();
            Camera = new ThirdPersonCamera();

            EnableAllCollisions = true;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;

            base.Respawn();
        }

        public void SpawnCorpse()
        {
            ModelEntity modelEntity = new();
            modelEntity.SetModel(Model);
            modelEntity.Position = EyePos + EyeRot.Forward * PositionVelocity;
            modelEntity.Rotation = Rotation.LookAt(Vector3.Random.Normal);
            modelEntity.SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
            modelEntity.PhysicsGroup.Velocity = EyeRot.Forward * PhysicGroupVelocity;

        }
    }
}