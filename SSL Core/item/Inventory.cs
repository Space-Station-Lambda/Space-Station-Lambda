using System;

namespace SSL_Core.item
{
    public class Inventory
    {
        public ItemStack[] Items { get; }

        public int SlotsCount => Items.Length;

        public int SlotsLeft
        {
            get
            {
                int slotsCount = SlotsCount;
                int slotsLeft = 0;

                for (var i = 0; i < slotsCount; i++)
                {
                    if (Items[i] == null)
                    {
                        slotsLeft++;
                    }
                }

                return slotsLeft;
            }
        }
        
        private ItemAuthorizer authorizer;

        public Inventory(IItems items, int size)
        {
            Items = new ItemStack[size];
            authorizer = new ItemAuthorizer(items);
        }

        /// <summary>
        /// Adds an ItemStack to a preferred position in the inventory.
        /// </summary>
        /// The ItemStack will be merged if the preferred position is the same Item.
        /// If the preferred position is not the same Item, it will add the stack to the next available slot.
        /// <param name="itemStack">Item stack to add</param>
        /// <param name="position">The preferred position</param>
        /// <exception cref="FullInventoryException">When the item stack can't be added because the inventory is full</exception>
        public void AddItem(ItemStack itemStack, int position = 0)
        {
            if (authorizer.IsAuthorized(itemStack.Item))
            {
                var slotsCount = SlotsCount;
                var itemAdded = false;
                
                if (IsSlotEmpty(position))
                {
                    Items[position] = itemStack;
                    itemAdded = true;
                } 
                else if (Items[position].Item.Equals(itemStack.Item))
                {
                    Items[position].Add(itemStack.Amount);
                    itemAdded = true;
                }
                else
                {
                    for (var i = 0; i < slotsCount && !itemAdded; i++)
                    {
                        if (IsSlotEmpty(i))
                        {
                            Items[i] = itemStack;
                            itemAdded = true;
                        }
                    }
                }

                if (!itemAdded)
                {
                    throw new FullInventoryException($"{itemStack} could not be added into the inventory.");
                }
            }
            else
            {
                throw new NotImplementedException("ItemAuthorizer/Filter needs a rework before");
            }
        }

        /// <summary>
        /// Checks if a specific slot is empty.
        /// </summary>
        /// <param name="position">Index of the slot</param>
        /// <returns>True if empty, false otherwise</returns>
        /// <exception cref="IndexOutOfRangeException">If the specified index is not inbound.</exception>
        public bool IsSlotEmpty(int position)
        {
            if (position < 0 || position >= SlotsCount)
            {
                throw new IndexOutOfRangeException($"There is only {SlotsCount} slots in the inventory.");
            }

            return Items[position] == null;
        }
    }
}