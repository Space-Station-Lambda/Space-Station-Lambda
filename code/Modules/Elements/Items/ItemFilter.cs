using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Modules.Elements.Items.Carriables;
using ssl.Modules.Elements.Items.Data;

namespace ssl.Modules.Elements.Items
{
    /// <summary>
    /// Filter items in a specific inventory/slot
    /// </summary>
    public partial class ItemFilter : BaseNetworkable
    {
        public ItemFilter()
        {
            if (!Host.IsServer) return;
            Blacklist = new List<ItemData>();
            Whitelist = new List<ItemData>();
        }
        
        /// <summary>
        /// Not authorized items
        /// </summary>
        [Net] protected List<ItemData> Blacklist { get; private set; }

        /// <summary>
        /// Authorized items
        /// </summary>
        [Net] protected List<ItemData> Whitelist { get; private set; }

        public bool IsAuthorized(ItemData item)
        {
            if (Whitelist.Contains(item)) return true;
            if (Blacklist.Contains(item)) return false;
            return !(Whitelist.Count > 0);
        }

        public bool IsAuthorized(Item item)
        {
            if (Enumerable.Any(Whitelist, data => data.Id == item.Data.Id)) return true;
            if (Enumerable.Any(Blacklist, data => data.Id == item.Data.Id)) return false;
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