namespace SSL_Core.model
{
    /// <summary>
    /// Système de faim. Décroissance progressive ici ? --
    /// </summary>
    public class FeedingJauge
    {
        private const int DefaultMaxFeeding = 100;
        public int MaxFeeding { get; }

        public int Feeding { get; private set; }
        
        public FeedingJauge(int feed = 0)
        {
            Feeding = feed;
            MaxFeeding = DefaultMaxFeeding;
            
        }

        /// <summary>
        /// Ajoute de la nourtiture, ne peux pas dépasser
        /// </summary>
        public void AddFeed(int value)
        {
            Feeding += value;
            
            if (Feeding > MaxFeeding)
            {
                Feeding = MaxFeeding;
            }
        }
    }
}