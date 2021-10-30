using Sandbox;
using ssl.Modules.Clothes;
using ssl.Modules.Inputs;
using ssl.Modules.Items;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Props.Types;
using ssl.Modules.Roles;
using ssl.Modules.Selection;
using ssl.Modules.Statuses;
using ssl.Player.Animators;
using ssl.Player.Cameras;
using ssl.Player.Controllers;
using SpawnPoint = ssl.Modules.Rounds.SpawnPoint;

namespace ssl.Player
{
    public partial class MainPlayer : Sandbox.Player, ISelectable
    {
        private const string Model = "models/citizen/citizen.vmdl";
        private const float MaxHealth = 100f;

        public MainPlayer()
        {
            Health = MaxHealth;
            Dragger = new Dragger(this);
            InputHandler = new InputHandler(this);

            if (Host.IsServer)
            {
                StainHandler = Components.Create<StainHandler>();
                ClothesHandler = Components.Create<ClothesHandler>();
                StatusHandler = Components.Create<StatusHandler>();
                RoleHandler = Components.Create<RoleHandler>();
                Inventory = Components.Create<PlayerInventory>();
            }
        }

        public new PlayerInventory Inventory { get; }
        public RoleHandler RoleHandler { get; }
        public ClothesHandler ClothesHandler { get; }
        public StatusHandler StatusHandler { get; }
        public StainHandler StainHandler { get; }
        public InputHandler InputHandler { get; }
        public Dragger Dragger { get; }
        public PlayerCorpse Ragdoll { get; set; }

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

        public override void ClientSpawn()
        {
            base.ClientSpawn();

            if (Inventory.ViewModel != null || !IsLocalPawn) return;
            Inventory.ViewModel = new HandViewModel
            {
                EnableAllCollisions = false,
                EnableViewmodelRendering = true,
                Owner = this.Owner
            };
            Inventory.ViewModel.SetHoldType(HoldType.None);
        }

        /// <summary>
        /// Called each tick, clientside and serverside
        /// </summary>
        /// <param name="client"></param>
        public override void Simulate(Client client)
        {
            PawnController controller = GetActiveController();
            controller?.Simulate(client, this, GetActiveAnimator());
            StatusHandler.Tick();
            SimulateActiveChild(client, ActiveChild);
            InputHandler.CheckControls();
            Dragger?.UpdateTarget();
            StainHandler.TryGenerateStain();
        }

        /// <summary>
        /// Called on respawn
        /// </summary>
        public override void Respawn()
        {
            SetModel(Model);

            Controller = new HumanController();
            Animator = new HumanAnimator();
            Camera = new FirstPersonCamera();

            EnableAllCollisions = true;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;

            Inventory.Clear();

            RoleHandler.SpawnRole();

            SendTextNotification("You are " + RoleHandler.Role.Name);

            base.Respawn();
        }

        public void Respawn(Vector3 position, Rotation rotation)
        {
            Respawn();

            Position = position;
            Rotation = rotation;
        }

        public void Respawn(SpawnPoint spawnPoint)
        {
            Respawn(spawnPoint.Position, spawnPoint.Rotation);
        }

        public override void OnKilled()
        {
            LifeState = LifeState.Dead;
            StopUsing();
            RoleHandler.Role?.OnKilled(this);
            EnableRagdoll(Vector3.Zero, 0);
            Gamemode.Instance.RoundManager.CurrentRound.OnPlayerKilled(this);
        }

        public override void PostCameraSetup(ref CameraSetup setup)
        {
            base.PostCameraSetup(ref setup);
            Inventory.ViewModel.PostCameraSetup(ref setup);
        }

        public void EnableSpectator()
        {
            Host.AssertServer();
            
            Controller = null;
            Animator = null;
            
            SpectatorCamera specCam = new();
            Camera = specCam;
            
            EnableAllCollisions = false;
            EnableDrawing = false;
        }

        private void EnableRagdoll(Vector3 force, int forceBone)
        {
            PlayerCorpse ragdoll = new()
            {
                Position = Position,
                Rotation = Rotation
            };

            ragdoll.CopyFrom(this);
            ragdoll.ApplyForceToBone(force, forceBone);
            ragdoll.Player = this;

            Ragdoll = ragdoll;
        }

        [ClientRpc]
        private void SendTextNotification(string txt)
        {
            Log.Info("Trying to start event");
            Event.Run("ssl.notification", txt);
        }
    }
}