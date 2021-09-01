using System;
using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Items
{
    public partial class PlayerInventory : Inventory
    {
        private const int MaxInventoryCapacity = 10;

        public Item HoldingItem => HoldingSlot?.Item;
        public Slot HoldingSlot { get; private set; }
        [Net, Predicted] public int HoldingSlotNumber { get; private set; }
        [Net] private MainPlayer player { get; set; }
        public HandViewModel ViewModel { get; set; }

        public PlayerInventory()
        {
        }
        
        public PlayerInventory(MainPlayer player) : base(MaxInventoryCapacity)
        {
            this.player = player;
        }

        public void ProcessHolding(int slotIndex)
        {
            if (HoldingSlotNumber == slotIndex) StopHolding();
            else StartHolding(Slots[slotIndex]);
        }
        
        public void StartHolding(Slot slot)
        {
            StopHolding();
            HoldingSlot = slot;
            HoldingItem?.ActiveStart(player);
            HoldingItem?.SetModel(HoldingItem.Model);
            HoldingItem?.OnCarryStart(player);
            player.ActiveChild = HoldingItem;
            HoldingSlotNumber = Slots.IndexOf(slot);
            
            if (Host.IsClient)
            {
                RefreshViewModel();
            }
        }
        
        public void StopHolding()
        {
            HoldingItem?.ActiveEnd(player, false);
            HoldingSlot = null;
            HoldingSlotNumber = -1;
            if (Host.IsClient) RefreshViewModel();
        }

        public Item DropItem()
        {
            Item droppedItem = HoldingItem;
            HoldingItem?.OnCarryDrop(player);
            HoldingItem?.ActiveEnd(player, true);
            HoldingSlot.Clear();
            droppedItem.Velocity += player.Velocity;
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

        /// <summary>
        /// Updates the view model's hold type to match the holding item
        /// </summary>
        private void RefreshViewModel()
        {
            HoldType holdingType = HoldingItem?.HoldType ?? HoldType.None;
            ViewModel.SetHoldType(holdingType);
        }
    }
}