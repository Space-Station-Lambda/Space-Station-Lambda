﻿using SSL_Core.exception;

namespace SSL_Core.model.items
{
    public class ItemStack
    {
        public Item Item { get; }
        public int Amount { get; private set; }

        public ItemStack(Item item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }
        
        public ItemStack Remove(int number)
        {
            if (number > Amount)
            {
                throw new NegativeItemStackException();
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
                throw new OutOfStackItemStackException();
            }
            
            Amount += number;
        }

        public override string ToString()
        {
            return $"{Item} ({Amount})";
        }
    }
}
