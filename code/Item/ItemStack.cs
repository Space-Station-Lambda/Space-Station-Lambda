using System;

namespace ssl.Item
{
    public class ItemStack
    {
        public ItemStack(ItemTypes.ItemCore itemCore, int amount = 1)
        {
            ItemCore = itemCore;
            Amount = amount;
        }

        public ItemTypes.ItemCore ItemCore { get; }
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

            return new ItemStack(ItemCore, number);
        }

        public void Add(int number)
        {
            if (Amount + number > ItemCore.MaxStack)
            {
                throw new Exception();
            }

            Amount += number;
        }

        public override string ToString()
        {
            return $"{ItemCore} ({Amount})";
        }
    }
}