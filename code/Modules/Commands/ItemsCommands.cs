using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Items.Instances;
using ssl.Player;

namespace ssl.Modules.Commands;

public class ItemsCommands
{
	/// <summary>
	///     Clear the inventory
	/// </summary>
	[AdminCmd("inv_clear")]
    public static void ClearInventory()
    {
        Client client = ConsoleSystem.Caller;
        SslPlayer sslPlayer = (SslPlayer) client.Pawn;
        sslPlayer.Inventory.Clear();
        Log.Info("Your inventory is now clear.");
    }

	/// <summary>
	///     give an item
	/// </summary>
	[AdminCmd("inv_give")]
    public static void GiveItem(string id)
    {
        Client client = ConsoleSystem.Caller;
        SslPlayer sslPlayer = (SslPlayer) client.Pawn;
        ItemFactory itemFactory = ItemFactory.Instance;
        try
        {
            Item item = itemFactory.Create(id);
            if (null != sslPlayer.Inventory.Add(item)) return;

            Log.Info($"Cannot give {id}, inventory full.");
            item.Delete();
        }
        catch
        {
            Log.Info($"{id} not found.");
        }
    }

	/// <summary> Create item with its id </summary>
	/// <param name="id"> The id of the item </param>
	[AdminCmd("item")]
    public static void SpawnItem(string id)
    {
        Client client = ConsoleSystem.Caller;
        SslPlayer sslPlayer = (SslPlayer) client.Pawn;
        ItemFactory itemFactory = ItemFactory.Instance;
        try
        {
            Item item = itemFactory.Create(id);
            item.Position = sslPlayer.EyePosition + sslPlayer.EyeRotation.Forward * 50;
            item.Rotation = sslPlayer.EyeRotation;

            Log.Info($"{item} has been spawned.");
        }
        catch
        {
            Log.Info($"{id} not found.");
        }
    }
}