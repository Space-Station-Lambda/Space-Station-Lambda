using System.Collections.Generic;
using SSL.item.items;

namespace SSL.item
{
    public interface IItems
    {
        List<Item> GetItems();
        bool Contains(string id);
        bool Contains(Item item);
        Item Get(string id);
        Item[] GetByType(string type);
        void Add(Item item);
    }
}