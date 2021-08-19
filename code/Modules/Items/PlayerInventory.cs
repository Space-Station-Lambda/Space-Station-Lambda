using System;
using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Items
{
    public partial class PlayerInventory : Inventory
    {
        public event Action<int> SlotSelected;
        
        public Item HoldingItem => HoldingSlot?.Item;
        [Net] public Slot HoldingSlot { get; private set; }
        
        [Net] private MainPlayer player { get; set; }
        
        public PlayerInventory()
        {
        }

        public PlayerInventory(MainPlayer player) : base(10)
        {
            this.player = player;
        }

        /// <summary>
        /// When the player change selected slot
        /// </summary>
        /// <param name="slotIndex">The current slot selected</param>
        [ServerCmd("set_inventory_holding")]
        private static void SetInventoryHolding(int slotIndex)
        {
            MainPlayer target = (MainPlayer)ConsoleSystem.Caller.Pawn;
            target?.Inventory.StartHolding(slotIndex);
        }

        public void StartHolding(int slotIndex)
        {
            StartHolding(Slots[slotIndex]);
        }
        
        public void StartHolding(Slot slot)
        {
            if (IsClient) SetInventoryHolding(Slots.IndexOf(slot));
            
            HoldingItem?.ActiveEnd(player, false);
            HoldingSlot = slot;
            HoldingItem?.ActiveStart(player);
            HoldingItem?.SetModel(HoldingItem.Model);
            HoldingItem?.OnCarryStart(player);
            player.ActiveChild = HoldingItem;
            
            SlotSelected?.Invoke(Slots.IndexOf(slot));
        }

        public void DropItem()
        {
            HoldingItem?.OnCarryDrop(player);
            HoldingItem?.ActiveEnd(player, true);
            HoldingSlot.Clear();
        }
        
        /// <summary>
        /// Adds an ItemStack to a preferred position in the inventory.
        /// </summary>
        /// The ItemStack will be merged if the preferred position is the same Item.
        /// If the preferred position is not the same Item, it will add the stack to the next available slot.
        /// <param name="item">Item stack to add</param>
        /// <param name="position">The preferred position</param>
        /// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
        public void Add(Item item, int position = 0)
        {
            base.Add(item, position);
            StartHolding(HoldingSlot);
        }
    }
}