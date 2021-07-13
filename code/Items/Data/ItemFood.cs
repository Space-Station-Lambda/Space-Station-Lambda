using ssl.Gauges;
using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemFood : Item
    {
        public int FeedingValue { get; private set; }

        public ItemFood(string id, string name, int feedingValue) : base(id, name)
        {
            Name = name;
            Id = ".food." + id;
            FeedingValue = feedingValue;
            MaxStack = 1;
        }
        
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
