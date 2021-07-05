using ssl.Player;

namespace ssl.item.items
{
    public class ItemFood : Item
    {
        public ItemFood(string id, string name, int feedingValue) : base(id, name, "food", 100)
        {
            FeedingValue = feedingValue;
        }

        public int FeedingValue { get; private set; }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void Use(MainPlayer mainPlayer)
        {
            Gauge.Gauge gauge = mainPlayer.GaugeHandler.GetGauge("feeding");
            if (gauge.ValueLeft > FeedingValue)
            {
                gauge.AddValue(FeedingValue);
            }
        }
    }
}