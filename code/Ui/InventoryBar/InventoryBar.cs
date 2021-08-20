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

        [Event.BuildInput]
        private void ProcessClientInput(InputBuilder input)
        {
            PlayerInventory inventory = player.Inventory;
            if (input.Pressed(InputButton.Slot1)) inventory.StartHolding(0);
            if (input.Pressed(InputButton.Slot2)) inventory.StartHolding(1);
            if (input.Pressed(InputButton.Slot3)) inventory.StartHolding(2);
            if (input.Pressed(InputButton.Slot4)) inventory.StartHolding(3);
            if (input.Pressed(InputButton.Slot5)) inventory.StartHolding(4);
            if (input.Pressed(InputButton.Slot6)) inventory.StartHolding(5);
            if (input.Pressed(InputButton.Slot7)) inventory.StartHolding(6);
            if (input.Pressed(InputButton.Slot8)) inventory.StartHolding(7);
            if (input.Pressed(InputButton.Slot9)) inventory.StartHolding(8);
            if (input.Pressed(InputButton.Slot0)) inventory.StartHolding(9);
            if (input.MouseWheel != 0) inventory.StartHolding(
                selected + input.MouseWheel < 0 ? 
                inventory.SlotsCount - 1: 
                (selected + input.MouseWheel) % inventory.SlotsCount);
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