using System.Runtime.Serialization;
using SSL_Core.model.gauges;
using SSL_Core.model.player;

namespace SSL_Core.model.items
{
    [DataContract(Name = "ItemFood", Namespace = "items")]
    public class ItemFood : Item
    {
        [DataMember]
        public int FeedingValue { get; private set; }

        private ItemFood() : base()
        {
            
        }
        
        public ItemFood(string id, string name, int feedingValue) : base(id, name, 100)
        {
            FeedingValue = feedingValue;
        }

        /// <summary>
        /// Première version, la nourriture rassasie l'utilisateur d'un certain nombre
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