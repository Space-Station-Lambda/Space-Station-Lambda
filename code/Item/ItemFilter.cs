using System.Collections.Generic;

namespace ssl.item
{
    public class ItemFilter
    {
        public Dictionary<ItemTypes.ItemCore, bool> authorizations;
        private IItems Items;

        public ItemFilter(IItems items, bool authorized = true)
        {
            Items = items;
            authorizations = new Dictionary<ItemTypes.ItemCore, bool>();
            foreach (ItemTypes.ItemCore item in Items.GetItems())
            {
                authorizations.Add(item, true);
            }

            if (!authorized) UnauthorizeAll();
        }

        public void UnauthorizeAll()
        {
            foreach (KeyValuePair<ItemTypes.ItemCore, bool> pair in authorizations)
            {
                authorizations[pair.Key] = false;
            }
        }

        public void AuthorizeAll()
        {
            foreach (KeyValuePair<ItemTypes.ItemCore, bool> pair in authorizations)
            {
                authorizations[pair.Key] = true;
            }
        }

        public bool IsAuthorized(ItemTypes.ItemCore itemCore)
        {
            return authorizations[itemCore];
        }

        public bool IsAuthorized(string id)
        {
            return IsAuthorized(Items.Get(id));
        }

        public void SetAuthorization(ItemTypes.ItemCore itemCore, bool newValue)
        {
            authorizations[itemCore] = newValue;
        }

        public void SetAuthorization(string id, bool newValue)
        {
            SetAuthorization(Items.Get(id), newValue);
        }

        public void SetAuthorizationByType(string type, bool newValue)
        {
            foreach (ItemTypes.ItemCore item in Items.GetByType(type))
            {
                SetAuthorization(item, newValue);
            }
        }
    }
}