﻿// ReSharper disable UnassignedGetOnlyAutoProperty

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
    public partial class SslPlayer : Sandbox.Player, ISelectable
    {
        private const string Model = "models/citizen/citizen.vmdl";
        private const float MaxHealth = 100f;

        public SslPlayer()
        {
            Health = MaxHealth;

            if (Host.IsServer)
            {
                Components.Create<InputHandler>();
                Components.Create<Dragger>();
                Components.Create<RagdollHandler>();
                Components.Create<PlayerInventory>();
                Components.Create<RoleHandler>();
                Components.Create<ClothesHandler>();
                Components.Create<StatusHandler>();
                Components.Create<StainHandler>();
            }
        }
        [BindComponent] public new PlayerInventory Inventory  { get; }
        [BindComponent] public RoleHandler RoleHandler { get; }
        [BindComponent] public ClothesHandler ClothesHandler { get; }
        [BindComponent] public StatusHandler StatusHandler { get; }
        [BindComponent] public StainHandler StainHandler { get; }
        [BindComponent] public InputHandler InputHandler { get; }
        [BindComponent] public RagdollHandler RagdollHandler { get; }
        [BindComponent] public Dragger Dragger { get; }

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

        public override void ClientSpawn()
        {
            base.ClientSpawn();

            if (Inventory.ViewModel != null || !IsLocalPawn) return;
            Inventory.ViewModel = new HandViewModel
            {
                EnableAllCollisions = false,
                EnableViewmodelRendering = true,
                Owner = Owner
            };
            Inventory.ViewModel.SetHoldType(HoldType.None);
        }

        public override void FrameSimulate(Client cl)
        {
            base.FrameSimulate(cl);
            if (Inventory.HoldingItem.IsValid())
            {
                Inventory.HoldingItem.FrameSimulate(cl);
            }
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