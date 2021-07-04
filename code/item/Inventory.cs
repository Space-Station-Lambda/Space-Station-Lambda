using System;

namespace SSL.Item
{
    public class Inventory
    {
        public Slot[] Items { get; }

        public int Capacity;

        private ItemFilter filter;

        public Inventory(IItems items, int size)
        {
            Items = new Slot[size];
            filter = new ItemFilter(items);
        }

        public void AddItem(ItemStack itemStack)
        {
            throw new NotImplementedException();
        }
    }
}
