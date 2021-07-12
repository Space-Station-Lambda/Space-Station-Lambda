using ssl.Gauges;
using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemFood : Item
    {
        public int FeedingValue { get; private set; }

        public ItemFood(string id, string name, string model, int maxStack, int feedingValue) : base(id, name, model, maxStack, true)
        {
            FeedingValue = feedingValue;
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
