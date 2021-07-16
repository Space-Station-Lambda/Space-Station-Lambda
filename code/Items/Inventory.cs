﻿using System;

namespace ssl.Items
{
    public class Inventory
    {
        private ItemFilter filter;

        public Inventory(int size)
        {
            Items = new ItemStack[size];
            filter = new ItemFilter();
        }

        public ItemStack[] Items { get; }

        public int SlotsCount => Items.Length;

        public int SlotsLeft
        {
            get
            {
                var slotsLeft = 0;

                for (var i = 0; i < SlotsCount; i++)
                {
                    if (Items[i] == null)
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
        /// <exception cref="FullInventoryException">When the item stack can't be added because the inventory is full</exception>
        public void AddItem(ItemStack itemStack, int position = 0)
        {
            if (position < 0 || position >= SlotsCount)
            {
                throw new IndexOutOfRangeException($"There is only {SlotsCount} slots in the inventory.");
            }

            if (filter.IsAuthorized(itemStack.Item))
            {
                if (!IsPresent(itemStack))
                {
                    var slotsCount = SlotsCount;
                    var itemAdded = false;

                    for (var i = 0; i < slotsCount && !itemAdded; i++)
                    {
                        var j = (i + position) % SlotsCount;
                        if (IsSlotEmpty(j))
                        {
                            Items[j] = itemStack;
                            itemAdded = true;
                        }
                        else
                        {
                            if (Items[position].Item.Equals(itemStack.Item))
                            {
                                try
                                {
                                    Items[position].Add(itemStack.Amount);
                                    itemAdded = true;
                                }
                                catch (Exception)
                                {
                                    // ignored
                                }
                            }
                        }
                    }

                    if (!itemAdded)
                    {
                        throw new Exception($"{itemStack} could not be added into the inventory.");
                    }
                }
            }
            else
            {
                throw new NotImplementedException("ItemAuthorizer/Filter needs a rework before");
            }
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
                removedItem = Items[position];
                Items[position] = null;
            }

            return removedItem;
        }

        /// <summary>
        /// Checks if a specific slot is empty.
        /// </summary>
        /// <param name="position">Index of the slot</param>
        /// <returns>True if empty, false otherwise</returns>
        public bool IsSlotEmpty(int position)
        {
            return Items[position] == null;
        }

        /// <summary>
        /// Checks if an item stack is already in the inventory.
        /// </summary>
        /// It checks for the same reference and not only the same item.
        public bool IsPresent(ItemStack itemStack)
        {
            return Array.IndexOf(Items, itemStack) > -1;
        }
    }
}