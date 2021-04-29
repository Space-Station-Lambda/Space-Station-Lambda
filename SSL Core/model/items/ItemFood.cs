using SSL_Core.model.player;

namespace SSL_Core.model.items
{
    public class ItemFood : Item
    {
        
        public int FeedingValue { get; }
        
        public ItemFood(string id, string name, int feedingValue) : base(id, name, 100)
        {
            FeedingValue = feedingValue;
        }

        /// <summary>
        /// Première version, la nourriture rassasie l'utilisateur d'un certain nombre
        /// </summary>
        public override void Use(Player player)
        {
            player.Health.AddFeed(FeedingValue);
        }
    }
}