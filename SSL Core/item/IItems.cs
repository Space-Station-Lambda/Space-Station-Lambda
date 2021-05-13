using System.Collections.Generic;
using SSL_Core.item.items;

namespace SSL_Core.item
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