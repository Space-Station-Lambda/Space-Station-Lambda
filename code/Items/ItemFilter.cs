using System.Collections.Generic;
using ssl.Items.Data;
using Enumerable = System.Linq.Enumerable;

namespace ssl.Items
{
    /// <summary>
    /// Filter items in a specific inventory/slot
    /// </summary>
    public class ItemFilter
    {
        /// <summary>
        /// Not authorized items
        /// </summary>
        public HashSet<ItemData> Blacklist = new();

        /// <summary>
        /// Authorized items
        /// </summary>
        public HashSet<ItemData> Whitelist = new();

        public bool IsAuthorized(ItemData item)
        {
            if (Whitelist.Contains(item)) return true;
            if (Blacklist.Contains(item)) return false;
            return !(Whitelist.Count > 0);
        }
        
        public bool IsAuthorized(Item item)
        {
            if (Enumerable.Any(Whitelist, data => data.Id == item.Id)) return true;
            if (Enumerable.Any(Blacklist, data => data.Id == item.Id)) return false;
            return !(Whitelist.Count > 0);
        }

        public void AddToWhitelist(ItemData item)
        {
            Whitelist.Add(item);
        }

        public void RemoveFromWhitelist(ItemData item)
        {
            Whitelist.Remove(item);
        }

        public void AddToBlacklist(ItemData item)
        {
            Blacklist.Add(item);
        }

        public void RemoveFromBlacklist(ItemData item)
        {
            Blacklist.Remove(item);
        }
    }
}