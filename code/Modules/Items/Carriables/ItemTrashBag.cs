using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemTrashBag : Item
    {
        private const int InventorySize = 6;
        
        protected ItemTrashBag()
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
            player.Selector.CheckSelection();
        }
    }
}