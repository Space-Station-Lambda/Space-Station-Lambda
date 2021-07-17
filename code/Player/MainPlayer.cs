using Sandbox;
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
        private const int MaxInventoryCapacity = 10;
        private readonly ClothesHandler clothesHandler;

        public MainPlayer()
        {
            GaugeHandler = new GaugeHandler();
            Inventory = new Inventory(MaxInventoryCapacity);
            clothesHandler = new ClothesHandler(this);
        }

        public Role Role { get; private set; }
        [Net] public new Inventory Inventory { get; }
        [Net] public ItemStack Holding { get; set; }
        public GaugeHandler GaugeHandler { get; }

        
        /// <summary>
        /// When the player change selected slot
        /// </summary>
        /// <param name="slot">The current slot sleected</param>
        [ServerCmd("set_inventory_holding")]
        public static void SetInventoryHolding(int slot)
        {
            MainPlayer target = (MainPlayer)ConsoleSystem.Caller.Pawn;
            if (target == null) return;
            ItemStack itemStack = target.Inventory.Items[slot];
            target.Holding?.ActiveEnd(target, false);
            target.Holding = itemStack;
            target.Holding?.SetModel(target.Holding.Item.Model);
            target.Holding?.OnCarryStart(target);
            target.Holding?.ActiveStart(target);
        }
        
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
            
            //Current add items for testing purpose. 
            Inventory.AddItem(new ItemStack(Gamemode.Instance.ItemRegistry.GetItemById("weapon.pistol")), 4);
            Inventory.AddItem(new ItemStack(Gamemode.Instance.ItemRegistry.GetItemById("food.wine")), 1);
            Inventory.AddItem(new ItemStack(Gamemode.Instance.ItemRegistry.GetItemById("food.apple")), 6);
            Inventory.AddItem(new ItemStack(Gamemode.Instance.ItemRegistry.GetItemById("food.hotdog")), 9);
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


        [ClientRpc]
        private void InitRole()
        {
            Log.Info("Server ?" + Host.IsServer);

            clothesHandler.AttachClothes(Role.Clothing);
        }
    }
}