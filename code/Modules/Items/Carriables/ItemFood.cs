using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemFood : Item<ItemFoodData>
    {
        public ItemFood()
        {
        }

        public ItemFood(ItemFoodData itemData) : base(itemData)
        {
        }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void UseOn(MainPlayer player)
        {
            //TODO
        }
    }
}