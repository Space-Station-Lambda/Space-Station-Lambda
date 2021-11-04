using System;
using Sandbox;
using ssl.Modules.Clothes;
using ssl.Modules.Inputs;
using ssl.Modules.Roles;
using ssl.Modules.Selection;
using ssl.Modules.Statuses;
using ssl.Player.Animators;
using ssl.Player.Cameras;
using ssl.Player.Controllers;
using SpawnPoint = ssl.Modules.Rounds.SpawnPoint;

namespace ssl.Player
{
    public partial class Player : Sandbox.Player, ISelectable
    {
        private const string Model = "models/citizen/citizen.vmdl";
        private const float MaxHealth = 100f;

        public Player()
        {
            Health = MaxHealth;
            Dragger = new Dragger(this);
            InputHandler = new InputHandler(this);
            RagdollHandler = new RagdollHandler(this);
            
            if (Host.IsServer)
            {
                Components.Create<PlayerInventory>();
                Components.Create<RoleHandler>();
                Components.Create<ClothesHandler>();
                Components.Create<StatusHandler>();
                Components.Create<StainHandler>();
            }
        }

        public new PlayerInventory Inventory => Components.Get<PlayerInventory>();
        public RoleHandler RoleHandler => Components.Get<RoleHandler>();
        public ClothesHandler ClothesHandler => Components.Get<ClothesHandler>();
        public StatusHandler StatusHandler => Components.Get<StatusHandler>();
        public StainHandler StainHandler => Components.Get<StainHandler>();
        public InputHandler InputHandler { get; }
        public RagdollHandler RagdollHandler { get; }
        public Dragger Dragger { get; }

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

            Controller = new HumanController(this);
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
            RagdollHandler.SpawnRagdoll(Vector3.Zero, 0);
            Gamemode.Instance.RoundManager.CurrentRound.OnPlayerKilled(this);
        }

        public override void PostCameraSetup(ref CameraSetup setup)
        {
            base.PostCameraSetup(ref setup);
            Inventory.ViewModel?.PostCameraSetup(ref setup);
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
        

        [ClientRpc]
        private void SendTextNotification(string txt)
        {
            Log.Info("Trying to start event");
            Event.Run("ssl.notification", txt);
        }
    }
}