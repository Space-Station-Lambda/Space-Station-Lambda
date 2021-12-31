using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Modules.Items.Instances;

namespace ssl.Modules.Items;

/// <summary>
///     Filter items in a specific inventory/slot
/// </summary>
public partial class ItemFilter : BaseNetworkable
{
    public ItemFilter()
    {
        if (!Host.IsServer) return;

        Blacklist = new List<string>();
        Whitelist = new List<string>();
    }

    /// <summary>
    ///     Not authorized items
    /// </summary>
    [Net]
    protected IList<string> Blacklist { get; set; }

    /// <summary>
    ///     Authorized items
    /// </summary>
    [Net]
    protected IList<string> Whitelist { get; set; }

    public bool IsAuthorized(string id)
    {
        if (Whitelist.Contains(id)) return true;

        if (Blacklist.Contains(id)) return false;

        return !(Whitelist.Count > 0);
    }

    public bool IsAuthorized(Item item)
    {
        if (Whitelist.Any(id => id == item.Id)) return true;

        if (Blacklist.Any(id => id == item.Id)) return false;

        return !(Whitelist.Count > 0);
    }

    public void AddToWhitelist(string id)
    {
        Whitelist.Add(id);
    }

    public void RemoveFromWhitelist(string id)
    {
        Whitelist.Remove(id);
    }

    public void AddToBlacklist(string id)
    {
        Blacklist.Add(id);
    }

    public void RemoveFromBlacklist(string id)
    {
        Blacklist.Remove(id);
    }
}