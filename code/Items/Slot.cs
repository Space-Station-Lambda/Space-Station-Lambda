using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public class Slot : NetworkComponent
    {
        public ItemStack ItemStack { get; set; }

        public bool IsEmpty()
        {
            return ItemStack == null;
        }
        
        public bool IsFull()
        {
            return ItemStack.Amount >= ItemStack.Item.MaxStack;
        }

        /// <summary>
        /// Add an itemstack to the slot
        /// </summary>
        /// <param name="itemStack">Itemstack to add</param>
        /// <returns>Remaining itemstack</returns>
        public ItemStack Add(ItemStack itemStack)
        {
            ItemStack ??= new ItemStack(itemStack.Item, 0);
            return ItemStack.AddItemStack(itemStack);
        }

        public void Clear()
        {
            ItemStack = null;
        }
    }
}