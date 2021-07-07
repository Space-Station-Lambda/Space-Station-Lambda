using ssl.Player;

namespace ssl.Item.ItemTypes
{
    public class ItemFood : ItemCore
    {
        public int FeedingValue { get; private set; }
        
        public ItemFood(string id, string name, string model, int feedingValue) : base(id, name, "food", model, 100)
        {
            FeedingValue = feedingValue;
        }

        public int FeedingValue { get; private set; }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void UsedBy(MainPlayer player)
        {
            Gauge.Gauge gauge = player.GaugeHandler.GetGauge("feeding");
            if (gauge.ValueLeft > FeedingValue)
            {
                gauge.AddValue(FeedingValue);
            }
        }
    }
}