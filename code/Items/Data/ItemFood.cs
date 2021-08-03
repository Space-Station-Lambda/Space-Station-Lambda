using ssl.Gauges;
using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemFood : Item
    {
        public ItemFood(string id, string name, int feedingValue, string model) : base(id, name, model)
        {
            Name = name;
            Id = id;
            FeedingValue = feedingValue;
        }

        public int FeedingValue { get; }


        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void UsedBy(MainPlayer player, ItemStack itemStack)
        {
            Gauge gauge = player.GaugeHandler.GetGauge("feeding");
            if (gauge.ValueLeft > FeedingValue)
            {
                gauge.AddValue(FeedingValue);
            }
        }
    }
}