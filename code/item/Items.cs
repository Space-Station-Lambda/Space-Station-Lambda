using System;
using System.Collections.Generic;
using System.Linq;

namespace ssl.item
{
    public class Items : IItems
    {
        private readonly Dictionary<string, items.Item> items;

        public Items()
        {
            items = new Dictionary<string, items.Item>();
        }

        public List<items.Item> GetItems()
        {
            return items.Values.ToList();
        }

        public bool Contains(string id)
        {
            return items.ContainsKey(id);
        }

        public bool Contains(items.Item item)
        {
            return items.ContainsValue(item);
        }

        public items.Item Get(string id)
        {
            items.TryGetValue(id, out items.Item item);
            return item;
        }

        public items.Item[] GetByType(string type)
        {
            return items.Values.Where(item => item.Type.Equals(type)).ToArray();
        }

        public void Add(items.Item item)
        {
            if (Contains(item.Id))
            {
                throw new Exception();
            }

            items.Add(item.Id, item);
        }
    }
}