namespace SSL_Core.model.items
{
    public class ItemFood : Item
    {
        
        public int FeedingIndice { get; }
        
        public ItemFood(string id, string name, int feedingIndice) : base(id, name, 100)
        {
            FeedingIndice = feedingIndice;
        }

        /// <summary>
        /// Première version, la nourriture rassasie l'utilisateur d'un certain nombre
        /// </summary>
        public override void Use(Player player)
        {
            player.FeedingJauge.AddFeed(FeedingIndice);
        }
    }
}