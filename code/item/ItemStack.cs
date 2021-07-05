using System;

namespace ssl.item
{
    public class ItemStack
    {
        public ItemStack(items.Item item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }

        public items.Item Item { get; }
        public int Amount { get; private set; }

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

        public override string ToString()
        {
            return $"{Item} ({Amount})";
        }
    }
}