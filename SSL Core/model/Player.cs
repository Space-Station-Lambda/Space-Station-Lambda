namespace SSL_Core.model
{
    public class Player
    {

        public Role Role;   
        
        public Inventory Inventory { get; }

        private const int InitialCapacity = 100;
        
        public Player()
        {
            Inventory = new Inventory(InitialCapacity);
        }

    }
}