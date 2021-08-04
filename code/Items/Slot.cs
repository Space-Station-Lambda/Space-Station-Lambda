using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public partial class Slot : NetworkedEntityAlwaysTransmitted
    {
        [Net] public ItemStack ItemStack { get; set; }

        public bool IsEmpty()
        {
            return ItemStack == null;
        }
        
        public bool IsFilled()
        {
            return ItemStack != null;
        }

        /// <summary>
        /// Set an itemstack to the slot
        /// </summary>
        /// <param name="itemStack">Itemstack to set</param>
        public void Set(ItemStack itemStack)
        {
            ItemStack = itemStack;
        }

        public void Clear()
        {
            ItemStack = null;
        }
    }
}
