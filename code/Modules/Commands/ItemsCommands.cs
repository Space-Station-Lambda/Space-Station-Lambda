using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Commands
{
    public class ItemsCommands
    {
        /// <summary>
        /// Clear the inventory
        /// </summary>
        [AdminCmd("inv_clear")]
        public static void ClearInventory()
        {
            Client client = ConsoleSystem.Caller;
            MainPlayer player = (MainPlayer)client.Pawn;
            player.Inventory.Clear();
            Log.Info("Your inventory is now clear.");
        }
        /// <summary>
        /// give an item
        /// </summary>
        [AdminCmd("inv_give")]
        public static void GiveItem(string id)
        {
            Client client = ConsoleSystem.Caller;
            MainPlayer player = (MainPlayer)client.Pawn;
            ItemFactory itemFactory = new();
            try
            {
                Item item = itemFactory.Create(id);
                player.Inventory.Add(item);
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
            MainPlayer player = (MainPlayer) client.Pawn;
            ItemFactory itemFactory = new();
            try
            {
                Item item = itemFactory.Create(id);
                item.Position = player.EyePos + player.EyeRot.Forward * 50;
                item.Rotation = player.EyeRot;
                
                Log.Info($"{item} has been spawned.");
            }
            catch
            {
                Log.Info($"{id} not found.");
            }
        }
    }
}
