using System.Collections.Generic;
using SSL_Core.model.item.items;

namespace SSL_Core.model.item
{
    public class ItemAuthorizer
    {
        public Dictionary<Item, bool> authorizations;

        public ItemAuthorizer(bool authorized = true)
        {
            authorizations = new Dictionary<Item, bool>();
            foreach (Item item in Items.Instance.GetItems())
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
    }
}