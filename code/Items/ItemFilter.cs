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
        public HashSet<string> Blacklist = new();

        /// <summary>
        /// Authorized items
        /// </summary>
        public HashSet<string> Whitelist = new();

        public bool IsAuthorized(string itemId)
        {
            if (Whitelist.Contains(itemId)) return true;
            if (Blacklist.Contains(itemId)) return false;
            return !(Whitelist.Count > 0);
        }

        public void AddToWhitelist(string item)
        {
            Whitelist.Add(item);
        }

        public void RemoveFromWhitelist(string item)
        {
            Whitelist.Remove(item);
        }

        public void AddToBlacklist(string item)
        {
            Blacklist.Add(item);
        }

        public void RemoveFromBlacklist(string item)
        {
            Blacklist.Remove(item);
        }
    }
}