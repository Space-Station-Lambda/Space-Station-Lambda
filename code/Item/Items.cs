using System;
using System.Collections.Generic;
using System.Linq;

namespace ssl.item
{
    public class Items : IItems
    {
        private readonly Dictionary<string, ItemTypes.ItemCore> items;

        public Items()
        {
            items = new Dictionary<string, ItemTypes.ItemCore>();
        }

        public List<ItemTypes.ItemCore> GetItems()
        {
            return items.Values.ToList();
        }

        public bool Contains(string id)
        {
            return items.ContainsKey(id);
        }

        public bool Contains(ItemTypes.ItemCore itemCore)
        {
            return items.ContainsValue(itemCore);
        }

        public ItemTypes.ItemCore Get(string id)
        {
            items.TryGetValue(id, out ItemTypes.ItemCore item);
            return item;
        }

        public ItemTypes.ItemCore[] GetByType(string type)
        {
            return items.Values.Where(item => item.Type.Equals(type)).ToArray();
        }

        public void Add(ItemTypes.ItemCore itemCore)
        {
            if (Contains(itemCore.Id))
            {
                throw new Exception();
            }

            items.Add(itemCore.Id, itemCore);
        }
    }
}