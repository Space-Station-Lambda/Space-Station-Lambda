using Sandbox;
using ssl.Effects;
using ssl.Gauge;
using ssl.Items.Data;
using ssl.Player.Roles;

namespace ssl.Player
{
    public class MainPlayer : Sandbox.Player, IEffectable<MainPlayer>
    {
        private const string Model = "models/citizen/citizen.vmdl";
        private ClothesHandler clothesHandler;
        public Role Role;

        public MainPlayer()
        {
            GaugeHandler = new GaugeHandler();
            clothesHandler = new ClothesHandler(this);
        }
        
        public GaugeHandler GaugeHandler { get; }

        public void Apply(Effect<MainPlayer> effect)
        {
            effect.Trigger(this);
        }

        /// <summary>
        /// Makes the player use the item.
        /// </summary>
        public void Use(Item item)
        {
            item.UsedBy(this);
        }

        /// <summary>
        /// Called each tick, clientside and serverside
        /// </summary>
        /// <param name="client"></param>
        public override void Simulate(Client client)
        {
            base.Simulate(client);
            SimulateActiveChild(client, ActiveChild);
            CheckControls();
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

        private void CheckControls()
        {
            if (IsServer)
            {
                ServerControls();
            }

            if (IsClient)
            {
                ClientControls();
            }
        }

        private void ServerControls()
        {
            if (Input.Pressed(InputButton.Reload))
            {
                Respawn();
            }
        }

        private void ClientControls()
        {
        }

        public void SetRole(Role role)
        {
            clothesHandler.AttachClothes(role.Clothing);
        }
    }
}