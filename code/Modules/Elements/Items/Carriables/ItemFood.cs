using Sandbox;
using ssl.Modules.Elements.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Elements.Items.Carriables
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
        public override void OnUsePrimary(Player.SslPlayer sslPlayer, ISelectable target)
        {
            OnCarryDrop(this);
            ActiveEnd(sslPlayer, false);
            sslPlayer.Inventory.RemoveItem(this);
            
            if (!string.IsNullOrWhiteSpace(Data.WasteItem))
            {
                ItemFactory factory = new();
                Item waste = factory.Create(Data.WasteItem);
                sslPlayer.Inventory.Add(waste);
            }
            
            PlayEatSound(sslPlayer);
            if(Host.IsServer) Delete();
        }

        [ClientRpc]
        protected void PlayEatSound(Entity entity)
        {
            Sound.FromEntity(EatSound, entity);
        }
    }
}