using System.Collections.Generic;
using System.Linq;
using SSL_Core.exception;
using SSL_Core.model.item.items;

namespace SSL_Core.model.item
{
    public class Items
    {
        public static Items Instance => instance ??= new Items();
        private static Items instance;
        
        private Dictionary<string, Item> items;

        private Items()
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