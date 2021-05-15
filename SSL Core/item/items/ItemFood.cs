using System.Runtime.Serialization;
using SSL_Core.gauges;
using SSL_Core.player;

namespace SSL_Core.item.items
{
    [DataContract(Name = "ItemFood", Namespace = "items")]
    public class ItemFood : Item
    {
        [DataMember]
        public int FeedingValue { get; private set; }

        private ItemFood() : base()
        {
            
        }
        
        public ItemFood(string id, string name, int feedingValue) : base(id, name, "food", 100)
        {
            FeedingValue = feedingValue;
        }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void Use(Player player)
        {
            Gauge gauge = player.GetGauge("hunger");
            if (gauge.ValueLeft > FeedingValue)
            {
                gauge.AddValue(FeedingValue);  
            }
        }
    }
}