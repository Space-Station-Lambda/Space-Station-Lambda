﻿using Sandbox;
using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items
{
    public partial class Slot : NetworkedEntityAlwaysTransmitted
    {
        [Net] public Item Item { get; set; }

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
            item.Owner = this;
        }

        public void Clear()
        {
            Item = null;
        }
    }
}