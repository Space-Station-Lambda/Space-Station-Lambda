using System;
using Sandbox;
using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items
{
    public partial class Slot : NetworkedEntityAlwaysTransmitted
    {
        internal event Action<Slot> ItemAdded;
        internal event Action<Slot> ItemRemoved;
        
        [Net] public Item Item { get; private set; }

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
            FireItemAddedEvent();
        }

        public void Clear()
        {
            Item = null;
            FireItemRemovedEvent();
        }

        private void FireItemAddedEvent()
        {
            Log.Trace($"[Slot] FireItemAddedEvent");
            
            ItemAdded?.Invoke(this);
            OnItemAdded();
        }

        private void FireItemRemovedEvent()
        {
            Log.Trace($"[Slot] FireItemRemovedEvent");
            
            ItemRemoved?.Invoke(this);
            OnItemRemoved();
        }

        [ClientRpc]
        private void OnItemAdded()
        {
            ItemAdded?.Invoke(this);
        }

        [ClientRpc]
        private void OnItemRemoved()
        {
            ItemRemoved?.Invoke(this);
        }
    }
}