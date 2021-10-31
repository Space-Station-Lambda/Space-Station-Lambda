using Sandbox;
using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemFood : Item<ItemFoodData>
    {
        private const string EatSound = "grunt1";
        
        public ItemFood()
        {
        }

        public ItemFood(ItemFoodData itemData) : base(itemData)
        {
        }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void OnUsePrimary(MainPlayer player, ISelectable target)
        {
            OnCarryDrop(this);
            ActiveEnd(player, false);
            player.Inventory.RemoveItem(this);
            
            if (!string.IsNullOrWhiteSpace(Data.WasteItem))
            {
                ItemFactory factory = new();
                Item waste = factory.Create(Data.WasteItem);
                player.Inventory.Add(waste);
            }
            
            PlayEatSound(player);
            if(Host.IsServer) Delete();
        }

        [ClientRpc]
        protected void PlayEatSound(Entity entity)
        {
            Sound.FromEntity(EatSound, entity);
        }
    }
}