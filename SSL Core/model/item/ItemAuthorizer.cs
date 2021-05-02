using System.Collections.Generic;
using SSL_Core.exception;
using SSL_Core.model.item.items;

namespace SSL_Core.model.item
{
    public class ItemAuthorizer
    {
        public Dictionary<Item, bool> authorizations;
        private IItems Items;
        public ItemAuthorizer(IItems items, bool authorized = true)
        {
            Items = items;
            authorizations = new Dictionary<Item, bool>();
            foreach (Item item in Items.GetItems())
            {
                authorizations.Add(item, true);
            }
            if(!authorized) UnothorizeAll();
        }

        public void UnothorizeAll()
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
            if (!authorizations.TryGetValue(item, out bool res))
            {
                throw new ItemNotFoundException();
            }

            return res;
        }
        
        public bool IsAuthorized(string id)
        {
            return IsAuthorized(Items.Get(id));
        }

        public void SetAuthorization(Item item, bool newValue)
        {
            if (!authorizations.ContainsKey(item))
            {
                throw new ItemNotFoundException();
            }

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