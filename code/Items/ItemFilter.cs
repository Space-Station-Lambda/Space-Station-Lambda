using System.Collections.Generic;
using ssl.Items.Data;

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
        public HashSet<Item> Blacklist = new();

        /// <summary>
        /// Authorized items
        /// </summary>
        public HashSet<Item> Whitelist = new();

        public bool IsAuthorized(Item item)
        {
            if (Whitelist.Contains(item)) return true;
            if (Blacklist.Contains(item)) return false;
            return !(Whitelist.Count > 0);
        }

        public void AddToWhitelist(Item item)
        {
            Whitelist.Add(item);
        }

        public void RemoveFromWhitelist(Item item)
        {
            Whitelist.Remove(item);
        }

        public void AddToBlacklist(Item item)
        {
            Blacklist.Add(item);
        }

        public void RemoveFromBlacklist(Item item)
        {
            Blacklist.Remove(item);
        }
    }
}