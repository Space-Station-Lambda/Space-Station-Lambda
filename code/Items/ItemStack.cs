using System;
using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public partial class ItemStack : BaseCarriable
    {
        public ItemStack()
        {
        }

        public ItemStack(Item item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }

        public Item Item
        {
            get => item;
            private set
            {
                item = value;
                _itemId = value.Id;
            }
        }

        [Net] public int Amount { get; private set; }

        // This property exists to allow Item to be networked by its id only
        // The _ is here to have a better callback method name since it is a private field
        [Net, OnChangedCallback] private string _itemId { get; set; } 
        private Item item;

        public ItemStack Remove(int number)
        {
            if (number > Amount)
            {
                throw new Exception();
            }

            if (number == Amount)
            {
                return this;
            }

            Amount -= number;

            return new ItemStack(Item, number);
        }

        public void Add(int number)
        {
            if (Amount + number > Item.MaxStack)
            {
                throw new Exception();
            }

            Amount += number;
        }

        /// <summary>
        /// Callback executed to keep the Item property synced
        /// </summary>
        private void On_itemIdChanged()
        {
            Log.Info($"{(IsClient ? "Client" : "Server")} {_itemId}");
            Item = Gamemode.Instance.ItemRegistry.GetItemById(_itemId);
        }

        public override string ToString()
        {
            return $"{Item} ({Amount})";
        }
    }
}