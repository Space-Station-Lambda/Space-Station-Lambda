using Sandbox;
using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemTrashBag : Item
    {
        private const int InventorySize = 6;
        
        public ItemTrashBag()
        {
        }

        public ItemTrashBag(ItemData data) : base(data)
        {
            Content = new Inventory(InventorySize);
        }
        
        public Inventory Content { get; private set; }

        public override void UseOn(MainPlayer player)
        {
            base.UseOn(player);
            
            if (!Host.IsServer) return;
            if (player.Selector.Selected is Item item) AddToTrashBag(item);
        }

        /// <summary>
        /// Adds an item in the bag's inventory while hiding it in the world
        /// </summary>
        /// <param name="item"></param>
        private void AddToTrashBag(Item item)
        {
            Host.AssertServer();
            
            Slot destinationSlot = Content.Add(item);
            if (destinationSlot == null) return;
            
            item.SetParent(this, transform: Transform.Zero);
            item.EnableDrawing = false;
            item.PhysicsEnabled = false;
            item.EnableAllCollisions = false;
        }
    }
}