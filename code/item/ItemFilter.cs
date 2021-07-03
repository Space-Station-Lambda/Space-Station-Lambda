using System.Collections.Generic;
using SSL.item.items;

namespace SSL.item
{
    public class ItemFilter
    {
        public Dictionary<Item, bool> authorizations;
        private IItems Items;
        public ItemFilter(IItems items, bool authorized = true)
        {
            Items = items;
            authorizations = new Dictionary<Item, bool>();
            foreach (Item item in Items.GetItems())
            {
                authorizations.Add(item, true);
            }
            if(!authorized) UnauthorizeAll();
        }

        public void UnauthorizeAll()
        {
            foreach (KeyValuePair<Item, bool> pair in authorizations)
            {
                authorizations[pair.Key] = false;
            }
        }
        
        public void AuthorizeAll()
        {
            foreach (KeyValuePair<Item, bool> pair in authorizations)
            {
                authorizations[pair.Key] = true;
            }
        }

        public bool IsAuthorized(Item item)
        {
            return authorizations[item];
        }
        
        public bool IsAuthorized(string id)
        {
            return IsAuthorized(Items.Get(id));
        }

        public void SetAuthorization(Item item, bool newValue)
        {
            authorizations[item] = newValue;
        }
        
        public void SetAuthorization(string id, bool newValue)
        {
            SetAuthorization(Items.Get(id), newValue);
        }
        
        public void SetAuthorizationByType(string type, bool newValue)
        {
            foreach (Item item in Items.GetByType(type))
            {
                SetAuthorization(item, newValue);
            }
        }
    }
}
