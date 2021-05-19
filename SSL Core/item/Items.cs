using System.Collections.Generic;
using System.Linq;
using SSL_Core.exception;
using SSL_Core.item.items;

namespace SSL_Core.item
{
    public class Items : IItems
    {

        private readonly Dictionary<string, Item> items;

        public Items()
        {
            items = new Dictionary<string, Item>();
        }
        
        public List<Item> GetItems()
        {
            return items.Values.ToList();
        }
        
        public bool Contains(string id)
        {
            return items.ContainsKey(id);
        }
        
        public bool Contains(Item item)
        {
            return items.ContainsValue(item);
        }
        
        public Item Get(string id)
        {
            items.TryGetValue(id, out Item item);
            return item;
        }

        public Item[] GetByType(string type)
        {
            return items.Values.Where(item => item.Type.Equals(type)).ToArray();
        }
        
        public void Add(Item item)
        {
            if (Contains(item.Id))
            {
                throw new ItemAlreadyExistsException();
            }
            items.Add(item.Id, item);
        }
    }
}