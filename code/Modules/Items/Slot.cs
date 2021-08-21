using System;
using Sandbox;
using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items
{
    public class Slot
    {
        internal event Action<Slot> ItemAdded;
        internal event Action<Slot> ItemRemoved;
        
        public Item Item { get; private set; }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool IsFilled()
        {
            return Item != null;
        }

        /// <summary>
        /// Set an itemstack to the slot
        /// </summary>
        /// <param name="item">Itemstack to set</param>
        public void Set(Item item)
        {
            Item = item;
            ItemAdded?.Invoke(this);
        }

        public void Clear()
        {
            Item = null;
            ItemRemoved?.Invoke(this);
        }

        public override string ToString()
        {
            return IsEmpty() ? "-" : Item.ToString();
        }
    }
}