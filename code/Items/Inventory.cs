using System;
using System.Collections;
using System.Collections.Generic;
using Sandbox;

namespace ssl.Items
{
    public partial class Inventory : NetworkedEntityAlwaysTransmitted
    {
        private ItemFilter filter;

        public Inventory()
        {
        }
        
        public Inventory(int size)
        {
            items = new List<ItemStack>(size);
            for (int i = 0; i < size; i++)
            {
                items.Add(null);
            }
            filter = new ItemFilter();
        }

        [Net] private List<ItemStack> items { get; set; }

        public int SlotsCount => items.Count;

        public int SlotsLeft
        {
            get
            {
                var slotsLeft = 0;

                for (var i = 0; i < SlotsCount; i++)
                {
                    if (items[i] == null)
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
                            items[j] = itemStack;
                            itemAdded = true;
                        }
                        else
                        {
                            if (items[position].Item.Equals(itemStack.Item))
                            {
                                try
                                {
                                    items[position].Add(itemStack.Amount);
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
                removedItem = items[position];
                items[position] = null;
            }

            return removedItem;
        }

        public ItemStack GetItem(int position)
        {
            if (position < 0 || position >= SlotsCount)
            {
                throw new IndexOutOfRangeException($"There is only {SlotsCount} slots in the inventory.");
            }

            return items[position];
        }

        /// <summary>
        /// Removes each ItemStack of all slots of the inventory.
        /// </summary>
        public void ClearInventory()
        {
            for (int i = 0; i < SlotsCount; i++)
            {
                items[i] = null;
            }
        }
        
        /// <summary>
        /// Checks if a specific slot is empty.
        /// </summary>
        /// <param name="position">Index of the slot</param>
        /// <returns>True if empty, false otherwise</returns>
        public bool IsSlotEmpty(int position)
        {
            return items[position] == null;
        }

        /// <summary>
        /// Checks if an item stack is already in the inventory.
        /// </summary>
        /// It checks for the same reference and not only the same item.
        public bool IsPresent(ItemStack itemStack)
        {
            return items.IndexOf(itemStack) > -1;
        }
    }
}
