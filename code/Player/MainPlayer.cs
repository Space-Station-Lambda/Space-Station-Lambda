using Sandbox;
using ssl.Gauge;
using ssl.Interfaces;
using ssl.status;

namespace ssl.Player
{
    public partial class MainPlayer : Sandbox.Player, IEffectable<MainPlayer>
    {
        private const string Model = "models/citizen/citizen.vmdl";
        private const int PositionVelocity = 40;
        private const int PhysicGroupVelocity = 40;
        private const int InitialCapacity = 100;

        public Role.Role Role;

        public MainPlayer()
        {
            StatusHandler = new StatusHandler<MainPlayer>();
            GaugeHandler = new GaugeHandler();
        }

        public StatusHandler<MainPlayer> StatusHandler { get; }
        public GaugeHandler GaugeHandler { get; }

        public void Apply(IEffect<MainPlayer> effect)
        {
            effect.Trigger(this);
        }

        /// <summary>
        /// Called each tick, clientside and serverside
        /// </summary>
        /// <param name="client"></param>
        public override void Simulate(Client client)
        {
            base.Simulate(client);
            //Simulate children (weapon ...)
            SimulateActiveChild(client, ActiveChild);
            if (IsServer && Input.Pressed(InputButton.Attack1))
            {
                ModelEntity modelEntity = new();
                modelEntity.SetModel(Model);
                modelEntity.Position = EyePos + EyeRot.Forward * PositionVelocity;
                modelEntity.Rotation = Rotation.LookAt(Vector3.Random.Normal);
                modelEntity.SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
                modelEntity.PhysicsGroup.Velocity = EyeRot.Forward * PhysicGroupVelocity;
            }
        }

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

        public override void OnKilled()
        {
            base.OnKilled();

            EnableDrawing = false;
        }
    }
}