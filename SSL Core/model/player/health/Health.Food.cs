namespace SSL_Core.model.player.health
{
    /// <summary>
    /// Système de faim non décroissant pour l'instant
    /// </summary>
    public partial class Health
    {
        private const int DefaultMaxFeeding = 100;
        public int MaxFeeding { get; }

        public int Feeding { get; private set; } = 0;
        
        public Health(int feed = 0)
        {
            Feeding = feed;
            MaxFeeding = DefaultMaxFeeding;
            
        }

        /// <summary>
        /// Ajoute de la nourtiture, ne peux pas dépasser la valeur maximale
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