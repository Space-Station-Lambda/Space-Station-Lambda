using Sandbox;
using Sandbox.UI;
using ssl.Modules.Items;
using ssl.Player;

namespace ssl.Ui.InventoryBar
{
    public class InventoryBar : Panel
    {
        private const int DefaultSlot = 0;

        
        private InventoryBarSlot[] icons = new InventoryBarSlot[10];
        private int selected;

        private MainPlayer player => (MainPlayer)Local.Pawn;
        
        public InventoryBar()
        {
            StyleSheet.Load("Ui/InventoryBar/InventoryBar.scss");
            
            for (int i = 0; i < 10; i++)
            {
                string name = (i + 1).ToString();
                if (i == 9) name = "0";
                icons[i] = new InventoryBarSlot(i, name, this);
            }

            Gamemode.Instance.PlayerAddedEvent += OnPlayerAdded;
        }

        private void OnPlayerAdded(MainPlayer player)
        {
            if (!player.IsLocalPawn) return;
            Log.Trace("[InventoryBar] Player Added, registering events");
            
            player.PlayerSpawned += OnPlayerSpawned;
            player.Inventory.SlotSelected += OnSlotSelected;
            player.Inventory.ItemAdded += OnItemUpdated;
            player.Inventory.ItemRemoved += OnItemUpdated;
        }

        private void OnPlayerSpawned()
        {
            RefreshAllModels();
            player.Inventory.StartHolding(DefaultSlot);
        }

        private void OnItemUpdated(int slotIndex, Slot slot)
        {
            icons[slotIndex].RefreshModel();
        }

        private void OnSlotSelected(int slotIndex, Slot slot)
        {
            icons[selected].SetClass("selected", false);
            selected = slotIndex;
            if (slotIndex < 0) selected = 9;
            if (slotIndex > 9) selected = 0;
            icons[slotIndex].SetClass("selected", true);
            icons[slotIndex].RefreshModel();
        }

        private void RefreshAllModels()
        {
            foreach (InventoryBarSlot icon in icons)
            {
                icon.RefreshModel();
            }
        }
    }
}