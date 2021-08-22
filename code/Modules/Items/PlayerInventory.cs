using System;
using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Items
{
    public partial class PlayerInventory : Inventory
    {
        private const int MaxInventoryCapacity = 10;
        
        public event Action<int, Slot> SlotSelected;

        public Item HoldingItem => HoldingSlot?.Item;
        public Slot HoldingSlot { get; private set; }
        
        [Net] private MainPlayer player { get; set; }

        public PlayerInventory()
        {
        }
        
        public PlayerInventory(MainPlayer player) : base(MaxInventoryCapacity)
        {
            this.player = player;
        }

        public void StartHolding(int slotIndex)
        {
            StartHolding(Slots[slotIndex]);
        }
        
        public void StartHolding(Slot slot)
        {
            HoldingItem?.ActiveEnd(player, false);
            HoldingSlot = slot;
            HoldingItem?.ActiveStart(player);
            HoldingItem?.SetModel(HoldingItem.Model);
            HoldingItem?.OnCarryStart(player);
            if(null != HoldingItem) player.ActiveChild = HoldingItem;
            SlotSelected?.Invoke(Slots.IndexOf(slot), slot);
        }

        public Item DropItem()
        {
            Item droppedItem = HoldingItem;
            HoldingItem?.OnCarryDrop(player);
            HoldingItem?.ActiveEnd(player, true);
            HoldingSlot.Clear();
            return droppedItem;
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