using System.Collections.Generic;

namespace ssl.item
{
    public class ItemFilter
    {
        public Dictionary<items.Item, bool> authorizations;
        private IItems Items;

        public ItemFilter(IItems items, bool authorized = true)
        {
            Items = items;
            authorizations = new Dictionary<items.Item, bool>();
            foreach (items.Item item in Items.GetItems())
            {
                authorizations.Add(item, true);
            }

            if (!authorized) UnauthorizeAll();
        }

        public void UnauthorizeAll()
        {
            foreach (KeyValuePair<items.Item, bool> pair in authorizations)
            {
                authorizations[pair.Key] = false;
            }
        }

        public void AuthorizeAll()
        {
            foreach (KeyValuePair<items.Item, bool> pair in authorizations)
            {
                authorizations[pair.Key] = true;
            }
        }

        public bool IsAuthorized(items.Item item)
        {
            return authorizations[item];
        }

        public bool IsAuthorized(string id)
        {
            return IsAuthorized(Items.Get(id));
        }

        public void SetAuthorization(items.Item item, bool newValue)
        {
            authorizations[item] = newValue;
        }

        public void SetAuthorization(string id, bool newValue)
        {
            SetAuthorization(Items.Get(id), newValue);
        }

        public void SetAuthorizationByType(string type, bool newValue)
        {
            foreach (items.Item item in Items.GetByType(type))
            {
                SetAuthorization(item, newValue);
            }
        }
    }
}