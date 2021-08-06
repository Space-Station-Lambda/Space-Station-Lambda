﻿using Sandbox;
using Sandbox.Rcon;
using ssl.Effects;
using ssl.Entities;
using ssl.Gauges;
using ssl.Items;
using ssl.Items.Data;
using ssl.Player.Controllers;
using ssl.Player.Roles;
using Input = Sandbox.Input;
using Item = ssl.Items.Carriables.Item;
using ItemWeapon = ssl.Items.Carriables.ItemWeapon;
using SpawnPoint = ssl.Entities.SpawnPoint;
    
namespace ssl.Player
{
    public partial class MainPlayer : Sandbox.Player, IEffectable<MainPlayer>
    {
        private const string Model = "models/citizen/citizen.vmdl";
        private const int MaxInventoryCapacity = 10;

        public MainPlayer()
        {
            if (Host.IsServer)
            {
                Inventory = new Inventory(MaxInventoryCapacity)
                {
                    Owner = this
                };
                GaugeHandler = new GaugeHandler();
                ClothesHandler = new ClothesHandler(this);
                RoleHandler = new RoleHandler(this);
            }
        }

        [Net] public new Inventory Inventory { get; private set; }
        [Net] public Item Holding { get; private set; }

        /**
         * Handlers
         */
        public GaugeHandler GaugeHandler { get; }


        public ClothesHandler ClothesHandler { get;}
        [Net] public RoleHandler RoleHandler { get; }
        public PlayerCorpse Ragdoll { get; set; }


        public void Apply(Effect<MainPlayer> effect)
        {
            effect.Trigger(this);
        }

        /// <summary>
        /// When the player change selected slot
        /// </summary>
        /// <param name="slot">The current slot sleected</param>
        [ServerCmd("set_inventory_holding")]
        public static void SetInventoryHolding(int slot)
        {
            MainPlayer target = (MainPlayer)ConsoleSystem.Caller.Pawn;
            if (target == null) return;
            Item item = target.Inventory.Get(slot);
            target.Holding = item;
            target.Holding?.SetModel(target.Holding.Model);
            target.Holding?.OnCarryStart(target);
            target.ActiveChild = target.Holding;
        }

        /// <summary>
        /// Called each tick, clientside and serverside
        /// </summary>
        /// <param name="client"></param>
        public override void Simulate(Client client)
        {
            PawnController controller = GetActiveController();
            controller?.Simulate( client, this, GetActiveAnimator() );
            
            SimulateActiveChild(client, ActiveChild);
            CheckControls();
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

            RoleHandler.Init();

            Inventory.Add(Gamemode.Instance.ItemRegistry.GetItemById("weapon.pistol"));

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
        
        [ServerCmd("kill_player")]
        private static void KillPlayer()
        {
            ConsoleSystem.Caller.Pawn.TakeDamage(new DamageInfo()
            {
                Damage = 100
            });
        }


        private void EnableRagdoll( Vector3 force, int forceBone )
        {
            PlayerCorpse ragdoll = new()
            {
                Position = Position,
                Rotation = Rotation
            };

            ragdoll.CopyFrom( this );
            ragdoll.ApplyForceToBone( force, forceBone );
            ragdoll.Player = this;

            Ragdoll = ragdoll;
        }
    }
}
