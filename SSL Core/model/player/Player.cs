using SSL_Core.model.player.health;
using SSL_Core.model.roles;

namespace SSL_Core.model.player
{
    public partial class Player
    {

        public Role Role;

        public Health Health { get; }
        
        public Inventory Inventory { get; }

        private const int InitialCapacity = 100;
        
        public Player()
        {
            Inventory = new Inventory(InitialCapacity);
            Health = new Health();
        }
        

    }
}