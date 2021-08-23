using System;
using Sandbox;
using ssl.Modules.Clothes;
using ssl.Modules.Items;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Roles;
using ssl.Modules.Selection;
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
            Inventory = new PlayerInventory(this);
            ClothesHandler = new ClothesHandler(this);
            RoleHandler = new RoleHandler(this);
            Selector = new PlayerSelector(this);
        }

        public event Action PlayerSpawned;
        [Net] public new PlayerInventory Inventory { get; private set; }
        public ClothesHandler ClothesHandler { get; }
        public RoleHandler RoleHandler { get; }
        public PlayerSelector Selector { get; }
        public PlayerCorpse Ragdoll { get; set; }

        /// <summary>
        /// Called each tick, clientside and serverside
        /// </summary>
        /// <param name="client"></param>
        public override void Simulate(Client client)
        {
            PawnController controller = GetActiveController();
            controller?.Simulate(client, this, GetActiveAnimator());

            SimulateActiveChild(client, ActiveChild);
            CheckControls(); 
            Selector?.CheckSelection();
        }

        /// <summary>
        /// Called on respawn
        /// </summary>
        public override void Respawn()
        {
            SetModel(Model);

            Controller = new HumanController();
            Animator = new StandardPlayerAnimator();
            Camera = new FirstPersonCamera();

            EnableAllCollisions = true;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;
            
            Inventory.Clear();
            
            RoleHandler.SpawnRole();

            PlayerSpawned?.Invoke();
            
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

        private void CheckControls()
        {
            if (Input.Pressed(InputButton.Slot1)) Inventory.StartHolding(0);
            if (Input.Pressed(InputButton.Slot2)) Inventory.StartHolding(1);
            if (Input.Pressed(InputButton.Slot3)) Inventory.StartHolding(2);
            if (Input.Pressed(InputButton.Slot4)) Inventory.StartHolding(3);
            if (Input.Pressed(InputButton.Slot5)) Inventory.StartHolding(4);
            if (Input.Pressed(InputButton.Slot6)) Inventory.StartHolding(5);
            if (Input.Pressed(InputButton.Slot7)) Inventory.StartHolding(6);
            if (Input.Pressed(InputButton.Slot8)) Inventory.StartHolding(7);
            if (Input.Pressed(InputButton.Slot9)) Inventory.StartHolding(8);
            if (Input.Pressed(InputButton.Slot0)) Inventory.StartHolding(9);
            
            if (IsServer) ServerControls();
            if (IsClient) ClientControls();
        }

        private void ServerControls()
        {
            if (Input.Pressed(InputButton.Reload))
            {
                Respawn();
            }
            if (Input.Pressed(InputButton.Drop))
            {
                Item dropped = Inventory.DropItem();
                dropped.Velocity += Velocity;
            }
            if (Input.Pressed(InputButton.Use))
            {
                Selector.UseSelected();
            }
        }

        private void ClientControls()
        {
        }

        [ServerCmd("kill_player")]
        private static void KillPlayer()
        {
            ConsoleSystem.Caller.Pawn.TakeDamage(new DamageInfo()
            {
                Damage = MaxHealth
            });
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

        public void OnSelectStart(MainPlayer player)
        {
            //TODO
        }

        public void OnSelectStop(MainPlayer player)
        {
            //TODO
        }

        public void OnSelect(MainPlayer player)
        {
            //TODO
        }

        public void OnAction(MainPlayer player)
        {
            //TODO
        }

        [ClientRpc]
        private void SendTextNotification(string txt)
        {
            Log.Info("Trying to start event");
            Event.Run("ssl.notification", txt);
        }
    }
}