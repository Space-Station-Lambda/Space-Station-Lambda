using System;
using SSL_Core.exception;

namespace SSL_Core.model
{
    public class Item
    {
        public ItemData Data { get; }
        public int Amount { get; private set; }

        public Item(ItemData data, int amount = 0)
        {
            Data = data;
            Amount = amount;
        }

        public Item Remove(int number)
        {
            if (number > Amount)
            {
                throw new NegativeItemSlotException();
            }

            Amount -= number;
            
            return new Item(Data, number);
        }

        public void Add(int number)
        {
            Amount += number;
        }
    }
}