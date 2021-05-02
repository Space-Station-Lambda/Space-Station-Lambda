namespace SSL_Core.model.item
{
    public class Inventory
    {
        public Slot[] Items { get; }

        public int Capacity;

        private ItemAuthorizer authorizer;

        public Inventory(int size)
        {
            Items = new Slot[size];
            authorizer = new ItemAuthorizer();
        }

        public void AddItem(ItemStack itemStack)
        {

        }
    }
}