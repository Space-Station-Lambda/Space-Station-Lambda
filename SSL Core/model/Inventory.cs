using System.Collections.Generic;
using SSL_Core.model.items;

namespace SSL_Core.model
{
    public class Inventory
    {
        public List<ItemStack> Items { get; }

        public int Capacity;

        public Inventory(int capacity)
        {
            Items = new List<ItemStack>();
            Capacity = capacity;
        }

        public void AddItem(ItemStack itemStack)
        {
            
        }
        
        
        
    }
}