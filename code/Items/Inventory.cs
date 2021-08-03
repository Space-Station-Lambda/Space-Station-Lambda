﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public partial class Inventory : NetworkedEntityAlwaysTransmitted
    {
        private readonly ItemFilter itemFilter = new();

        public Inventory()
        {
        }
        
        public Inventory(int size)
        {
            Slots = new List<Slot>(size);
            for (int i = 0; i < size; i++)
            {
                Slots.Add(new Slot());
            }
        }

        [Net] public List<Slot> Slots { get; private set; }

        public int SlotsCount => Slots.Count;

        public int SlotsLeft
        {
            get
            {
                int slotsLeft = 0;
                for (int i = 0; i < SlotsCount; i++)
                {
                    if (Slots[i].IsEmpty())
                    {
                        slotsLeft++;
                    }
                }
                return slotsLeft;
            }
        }

        public int SlotsFull => SlotsCount - SlotsLeft;

        /// <summary>
        /// Adds an ItemStack to a preferred position in the inventory.
        /// </summary>
        /// The ItemStack will be merged if the preferred position is the same Item.
        /// If the preferred position is not the same Item, it will add the stack to the next available slot.
        /// <param name="itemStack">Item stack to add</param>
        /// <param name="position">The preferred position</param>
        /// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
        public ItemStack AddItem(ItemStack itemStack, int position = 0)
        {
            if (position < 0 || position >= SlotsCount)
            {
                throw new IndexOutOfRangeException($"There is only {SlotsCount} slots in the inventory.");
            }
            
            if (itemFilter.IsAuthorized(itemStack.Item))
            {
                Slot destination = (Slots[position].IsEmpty()) ? Slots[position] : GetFirstEmptySlot();
                if (destination != null)
                {
                    destination.Set(itemStack);   
                    return itemStack;
                }
                return null;
            }

            throw new Exception("Not permission");
        }

        /// <summary>
        /// Adds an Item to a preferred position in the inventory.
        /// </summary>
        /// The Item will be merged if the preferred position is the same Item.
        /// If the preferred position is not the same Item, it will add the stack to the next available slot.
        /// <param name="item">Item to add</param>
        /// <param name="position">The preferred position</param>
        /// <param name="amount">Amount of items to add</param>
        /// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
        public ItemStack AddItem(Item item, int position = 0)
        {
            ItemStack itemStack = new(item);
            return AddItem(itemStack, position);
        }
        
        /// <summary>
        /// Adds an Item to a preferred position in the inventory by its id.
        /// </summary>
        /// The Item will be merged if the preferred position is the same Item.
        /// If the preferred position is not the same Item, it will add the stack to the next available slot.
        /// <param name="itemId">ItemId to add</param>
        /// <param name="position">The preferred position</param>
        /// <param name="amount">Amount of items to add</param>
        /// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
        public ItemStack AddItem(string itemId, int position = 0)
        {
            Item item = Item.All[itemId];
            return AddItem(item, position);
        }

        /// <summary>
        /// Removes an item stack from the specified position.
        /// </summary>
        /// <param name="position">The position to remove the stack from.</param>
        /// <returns>The removed item stack if there was any, null if not</returns>
        public ItemStack RemoveItem(int position)
        {
            ItemStack removedItem = null;

            if (!IsSlotEmpty(position))
            {
                removedItem = Slots[position].ItemStack;
                Slots[position].Clear();
            }

            return removedItem;
        }

        public ItemStack GetItem(int position)
        {
            if (position < 0 || position >= SlotsCount)
            {
                throw new IndexOutOfRangeException($"There is only {SlotsCount} slots in the inventory.");
            }

            return Slots[position].ItemStack;
        }

        public Slot GetFirstEmptySlot()
        {
            return Slots.FirstOrDefault(slot => slot.IsEmpty());
        }
        public ItemStack GetItem(Item item)
        {
            return (from slot in Slots where item.Equals(slot.ItemStack?.Item) select slot.ItemStack).FirstOrDefault();
        }
        
        public List<ItemStack> GetItems(Item item)
        {
            return (from slot in Slots where item.Equals(slot.ItemStack?.Item) select slot.ItemStack).ToList();
        }

        /// <summary>
        /// Removes each ItemStack of all slots of the inventory.
        /// </summary>
        public void ClearInventory()
        {
            for (int i = 0; i < SlotsCount; i++)
            {
                Slots[i].Clear();
            }
        }
        
        /// <summary>
        /// Checks if a specific slot is empty.
        /// </summary>
        /// <param name="position">Index of the slot</param>
        /// <returns>True if empty, false otherwise</returns>
        public bool IsSlotEmpty(int position)
        {
            return Slots[position].IsEmpty();
        }

        /// <summary>
        /// Checks if an item stack is already in the inventory.
        /// </summary>
        /// It checks for the same reference and not only the same item.
        public bool IsPresent(Item item)
        {
            return Slots.Any(slot => item.Equals(slot.ItemStack?.Item));
        }
    }
}
