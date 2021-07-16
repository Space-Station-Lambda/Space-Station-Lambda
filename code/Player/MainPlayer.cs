﻿using Sandbox;
using ssl.Effects;
using ssl.Gauges;
using ssl.Items;
using ssl.Items.Data;
using ssl.Player.Roles;

namespace ssl.Player
{
    public partial class MainPlayer : Sandbox.Player, IEffectable<MainPlayer>
    {
        private const string Model = "models/citizen/citizen.vmdl";
        private readonly ClothesHandler clothesHandler;

        public MainPlayer()
        {
            GaugeHandler = new GaugeHandler();
            Inventory = new Inventory(10);
            clothesHandler = new ClothesHandler(this);
        }

        public Role Role { get; private set; }
        public Inventory Inventory { get; private set; }
        public ItemStack Holding { get; private set; }
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
            
            InitRole();
            
            base.Respawn();
        }

        public override void OnKilled()
        {
            base.OnKilled();

            EnableDrawing = false;
        }
        
        public void AssignRole(Role role)
        {
            Role = role;
            Log.Info("Role " + role.Name + " selected");
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

        public void SetHolding(int slot)
        {
            ItemStack itemStack = Inventory.Items[slot];
            Holding?.ActiveEnd(this, true);
            Holding = itemStack;
            //ItemStack i = new ItemStack(new ItemFood("apple", "test", 10));
            //i.SetModel("weapons/rust_pistol/rust_pistol.vmdl");
            Log.Info("Hold " + Holding?.Item.Name);
            Holding?.SetModel(Holding.Item.Model);
            Holding?.OnCarryStart(this);
            Holding?.ActiveStart(this);
        }

        [ClientRpc]
        private void InitRole()
        {
            clothesHandler.AttachClothes(Role.Clothing);
            Inventory.AddItem(new ItemStack(Gamemode.Instance.ItemRegistery.GetItemById("food.vine")), 4);
            Inventory.AddItem(new ItemStack(Gamemode.Instance.ItemRegistery.GetItemById("food.coffee")), 3);
        }
    }
}