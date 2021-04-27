using System.Collections.Generic;

namespace SSL_Core.model
{
    public class Inventory
    {
        public List<Item> Items { get; }

        public int Capacity;

        public Inventory(int capacity)
        {
            Items = new List<Item>();
            Capacity = capacity;
        }

        public void AddItem(Item item)
        {
            
        }
        
        
        
    }
}