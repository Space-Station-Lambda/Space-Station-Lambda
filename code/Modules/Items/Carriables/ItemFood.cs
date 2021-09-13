using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemFood : Item
    {
        public ItemFood()
        {
        }

        public ItemFood(ItemFoodData foodData) : base(foodData)
        {
            FeedingValue = foodData.FeedingValue;
        }

        public int FeedingValue { get; protected set; }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void UseOn(MainPlayer player)
        {
            //TODO
        }
    }
}