using Sandbox;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Instances;

public class ItemTrashBag : Item
{
    private const int INVENTORY_SIZE = 6;

    public ItemTrashBag()
    {
        if (!Host.IsServer) return;

        ItemFilter filter = new();
        filter.AddToBlacklist(Id);

        Content = new Inventory(INVENTORY_SIZE, filter) { Enabled = true };
        Components.Add(Content);
    }

    public Inventory Content { get; }

    /// <summary>
    ///     Using the trash bag on an item on ground will add it to the trash bag
    /// </summary>
    public override void OnDownUsePrimary(SslPlayer sslPlayer, ISelectable target)
    {
        base.OnDownUsePrimary(sslPlayer, target);
        if (!Host.IsServer) return;
        if (sslPlayer.Dragger.Selected is Item item) AddToTrashBag(item);
    }

    /// <summary>
    ///     Adds an item in the bag's inventory and hide it in the world
    /// </summary>
    /// <param name="item">The item to add to the trash bag</param>
    private void AddToTrashBag(Item item)
    {
        Slot destinationSlot = Content.Add(item);
        if (null == destinationSlot) return;

        item.SetParent(this, transform: Transform.Zero);
        item.EnableDrawing = false;
        item.PhysicsEnabled = false;
        item.EnableAllCollisions = false;
    }
}