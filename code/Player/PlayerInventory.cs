﻿using System;
using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Items.Carriables;

namespace ssl.Player
{
    public partial class PlayerInventory : Inventory
    {
        private const int MaxInventoryCapacity = 10;

        public PlayerInventory()
        {
        }

        public PlayerInventory(MainPlayer player) : base(MaxInventoryCapacity)
        {
            this.Player = player;
        }

        public Item HoldingItem => HoldingSlot?.Item;
        public Slot HoldingSlot { get; private set; }
        [Net, Predicted] public int HoldingSlotNumber { get; private set; }
        [Net] private MainPlayer Player { get; set; }
        public HandViewModel ViewModel { get; set; }

        public void ProcessHolding(int slotIndex)
        {
            if (HoldingSlotNumber == slotIndex) StopHolding();
            else StartHolding(Slots[slotIndex]);
        }

        private void StartHolding(Slot slot)
        {
            StopHolding();
            HoldingSlot = slot;
            HoldingItem?.ActiveStart(Player);
            HoldingItem?.SetModel(HoldingItem.Data.Model);
            HoldingItem?.OnCarryStart(Player);
            Player.ActiveChild = HoldingItem;
            HoldingSlotNumber = Slots.IndexOf(slot);

            if (Host.IsClient)
            {
                RefreshViewModel();
            }
        }

        private void StopHolding()
        {
            HoldingItem?.ActiveEnd(Player, false);
            HoldingSlot = null;
            Player.ActiveChild = null;
            HoldingSlotNumber = -1;
            if (Host.IsClient) RefreshViewModel();
        }

        public Item DropItem()
        {
            Item droppedItem = HoldingItem;
            HoldingItem?.OnCarryDrop(Player);
            HoldingItem?.ActiveEnd(Player, true);
            HoldingSlot.Clear();
            if (null != droppedItem) droppedItem.Velocity += Player.Velocity;
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
        public override void Add(Item item, int position = 0)
        {
            base.Add(item, position);
            StartHolding(HoldingSlot);
        }

        /// <summary>
        /// Updates the view model's hold type to match the holding item
        /// </summary>
        private void RefreshViewModel()
        {
            //TODO
            if (HoldingItem != null)
            {
                HoldType holdingType = (HoldType)HoldingItem.Data.HoldType;
                ViewModel.SetHoldType(holdingType);
            }
        }
    }
}