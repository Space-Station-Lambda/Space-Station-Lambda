using ssl.Gauges;
using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemFood : Item
    {
        public ItemFood(ItemFoodData foodData) : base(foodData)
        {
            FeedingValue = foodData.FeedingValue;
        }

        public int FeedingValue { get; }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void UsedBy(MainPlayer player)
        {
            Gauge gauge = player.GaugeHandler.GetGauge("feeding");
            if (gauge.ValueLeft > FeedingValue)
            {
                gauge.AddValue(FeedingValue);
            }
        }
    }
}