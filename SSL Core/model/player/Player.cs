using SSL_Core.model.roles;

namespace SSL_Core.model
{
    public class Player
    {

        public Role Role;

        public FeedingJauge FeedingJauge { get; set; }
        
        public Inventory Inventory { get; }

        private const int InitialCapacity = 100;
        
        public Player()
        {
            Inventory = new(InitialCapacity);
            FeedingJauge = new();
        }
        

    }
}