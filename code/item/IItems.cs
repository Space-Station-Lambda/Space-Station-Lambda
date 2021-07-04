using System.Collections.Generic;

namespace SSL.Item
{
    public interface IItems
    {
        List<items.Item> GetItems();
        bool Contains(string id);
        bool Contains(items.Item item);
        items.Item Get(string id);
        items.Item[] GetByType(string type);
        void Add(items.Item item);
    }
}
