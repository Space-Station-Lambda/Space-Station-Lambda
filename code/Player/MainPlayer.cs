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
                Components.Create<PlayerInventory>();
                Components.Create<RoleHandler>();
                Components.Create<ClothesHandler>();
                Components.Create<StatusHandler>();
                Components.Create<StainHandler>();
                
                TimeExitRagdoll = Time.Now;
            }

        }

        public new PlayerInventory Inventory => Components.Get<PlayerInventory>();
        public RoleHandler RoleHandler => Components.Get<RoleHandler>();
        public ClothesHandler ClothesHandler => Components.Get<ClothesHandler>();
        public StatusHandler StatusHandler => Components.Get<StatusHandler>();
        public StainHandler StainHandler => Components.Get<StainHandler>();
        public InputHandler InputHandler { get; }
        public Dragger Dragger { get; }

        public bool IsRagdoll => Ragdoll != null;
        public bool CanStand => ((TimeSince)TimeExitRagdoll).Absolute >= 0;
        private PlayerCorpse Ragdoll { get; set; }
        public float TimeExitRagdoll { get; set; }

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
            SpawnRagdoll(Vector3.Zero, 0);
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

        /// <summary>
        /// Activates the ragdoll mode of the player.
        /// </summary>
        public void StartRagdoll()
        {
            Ragdoll ??= SpawnRagdoll(Velocity, -1);
            SetParent(Ragdoll);
            EnableAllCollisions = false;
            EnableShadowInFirstPerson = false;
            Camera = new AttachedCamera(Ragdoll, "eyes", Rotation.From(-90, 90, 180), EyePos);
        }

        /// <summary>
        /// Stop the ragdoll mode of the player.
        /// </summary>
        public void StopRagdoll()
        {
            if (!Ragdoll.IsValid) return;
            
            Position = Ragdoll.Position;
            Velocity = Vector3.Zero;
            
            SetParent(null);
            
            Ragdoll.Delete();
            Ragdoll = null;
            
            EnableAllCollisions = true;
            EnableShadowInFirstPerson = true;
            
            Camera = new FirstPersonCamera();
        }

        /// <summary>
        /// Spawns a ragdoll looking like the player.
        /// </summary>
        private PlayerCorpse SpawnRagdoll(Vector3 force, int forceBone)
        {
            PlayerCorpse ragdoll = new(this)
            {
                Position = Position,
                Rotation = Rotation
            };

            ragdoll.CopyFrom(this);
            ragdoll.ApplyForceToBone(force, forceBone);

            return ragdoll;
        }

        [ServerCmd("ragdoll")]
        private static void SetRagdoll(bool state)
        {
            MainPlayer player = (MainPlayer)ConsoleSystem.Caller.Pawn;
            if (state)
            {
                player.StartRagdoll();
            }
            else
            {
                player.StopRagdoll();
            }
        }

        [ClientRpc]
        private void SendTextNotification(string txt)
        {
            Log.Info("Trying to start event");
            Event.Run("ssl.notification", txt);
        }
    }
}