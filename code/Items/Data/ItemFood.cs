using ssl.Player;

namespace ssl.Items.Data
{
    public abstract class ItemFood : Item
    {

        public abstract int FeedingValue { get; }
        public override int MaxStack => 1;
        public override bool DestroyOnUse => true;

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