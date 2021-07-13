using ssl.Gauges;
using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemFood : Item
    {
        public int FeedingValue { get; private set; }

        public ItemFood(string id, string name, int feedingValue)
        {
            Name = name;
            Id = id;
            FeedingValue = feedingValue;
        }

        public override string Id { get; protected set; }
        public override string Name { get; protected set; }

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
