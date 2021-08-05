﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
                Slot slot = new()
                {
                    Owner = this
                };
                Slots.Add(slot);
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
        /// <param name="item">Item stack to add</param>
        /// <param name="position">The preferred position</param>
        /// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
        public Item Add(Item item, int position = 0)
        {
            if (position < 0 || position >= SlotsCount)
            {
                throw new IndexOutOfRangeException($"There is only {SlotsCount} slots in the inventory.");
            }
            
            if (itemFilter.IsAuthorized(item.Id))
            {
                Slot slotDestination = (Slots[position].IsEmpty()) ? Slots[position] : GetFirstEmptySlot();
                if (slotDestination != null)
                {
                    slotDestination.Set(item);   
                    return item;
                }
                return null;
            }

            throw new Exception("Not permission");
        }

        /// <summary>
        /// Adds an Item to a preferred position in the inventory by its id.
        /// </summary>
        /// The Item will be merged if the preferred position is the same Item.
        /// If the preferred position is not the same Item, it will add the stack to the next available slot.
        /// <param name="itemId">ItemId to add</param>
        /// <param name="position">The preferred position</param>
        /// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
        public Item Add(string itemId, int position = 0)
        {
            Item item = Item.All[itemId].create();
            return Add(item, position);
        }

        /// <summary>
        /// Adds an Item to a preferred position in the inventory from an ItemData.
        /// </summary>
        /// The Item will be merged if the preferred position is the same Item.
        /// If the preferred position is not the same Item, it will add the stack to the next available slot.
        /// <param name="data">Item to add</param>
        /// <param name="position">The preferred position</param>
        /// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
        public Item Add(ItemData data, int position = 0)
        {
            return Add(data.create(), position);
        }

        /// <summary>
        /// Removes an item stack from the specified position.
        /// </summary>
        /// <param name="position">The position to remove the stack from.</param>
        /// <returns>The removed item stack if there was any, null if not</returns>
        public Item RemoveItem(int position)
        {
            Item removedItem = null;

            if (!IsSlotEmpty(position))
            {
                removedItem = Slots[position].Item;
                Slots[position].Clear();
            }

            return removedItem;
        }

        public Item Get(int position)
        {
            if (position < 0 || position >= SlotsCount)
            {
                throw new IndexOutOfRangeException($"There is only {SlotsCount} slots in the inventory.");
            }

            return Slots[position].Item;
        }

        public Slot GetFirstEmptySlot()
        {
            return Slots.FirstOrDefault(slot => slot.IsEmpty());
        }
        public Item Get(string itemId)
        {
            return (from slot in Slots where itemId.Equals(slot.Item.Id) select slot.Item).FirstOrDefault();
        }
        
        public List<Item> GetItems(ItemData item)
        {
            return (from slot in Slots where item.Equals(slot.Item) select slot.Item).ToList();
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
            return Slots.Any(slot => item.Equals(slot.Item));
        }
    }
}
