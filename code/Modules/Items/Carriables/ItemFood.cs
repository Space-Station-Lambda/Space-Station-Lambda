using Sandbox;
using ssl.Modules.Gauges;
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

        [Net] public int FeedingValue { get; }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void UseOn(MainPlayer player)
        {
            Gauge gauge = player.GaugeHandler.GetGauge("feeding");
            if (gauge.ValueLeft > FeedingValue)
            {
                gauge.AddValue(FeedingValue);
            }
        }
    }
}