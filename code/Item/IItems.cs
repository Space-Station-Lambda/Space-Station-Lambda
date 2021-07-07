using System.Collections.Generic;

namespace ssl.Item
{
    public interface IItems
    {
        List<ItemTypes.ItemCore> GetItems();
        bool Contains(string id);
        bool Contains(ItemTypes.ItemCore itemCore);
        ItemTypes.ItemCore Get(string id);
        ItemTypes.ItemCore[] GetByType(string type);
        void Add(ItemTypes.ItemCore itemCore);
    }
}