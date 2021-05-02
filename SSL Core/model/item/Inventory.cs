namespace SSL_Core.model.item
{
    public class Inventory
    {
        public Slot[] Items { get; }

        public int Capacity;

        private ItemAuthorizer authorizer;

        public Inventory(IItems items, int size)
        {
            Items = new Slot[size];
            authorizer = new ItemAuthorizer(items);
        }

        public void AddItem(ItemStack itemStack)
        {

        }
    }
}