using System.Runtime.Serialization;
using SSL.gauges;
using SSL.player;

namespace SSL.item.items
{
    [DataContract(Name = "ItemFood", Namespace = "items")]
    [KnownType(typeof(ItemFood))]
    public class ItemFood : Item
    {
        [DataMember]
        public int FeedingValue { get; private set; }

        private ItemFood()
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
            Gauge gauge = player.GaugeHandler.GetGauge("feeding");
            if (gauge.ValueLeft > FeedingValue)
            {
                gauge.AddValue(FeedingValue);  
            }
        }
    }
}