using System;
using SSL_Core.exception;

namespace SSL_Core.model
{
    public abstract class ItemSlot
    {
        public ItemData Data { get; set; }
        public int Amount { get; private set; }

        public ItemSlot(ItemData data, int amount = 0)
        {
            Data = data;
            Amount = amount;
        }

        public void Remove(int number)
        {
            if (number > Amount)
            {
                throw new NegativeItemSlotException();
            }

            Amount -= number;
            return
        }
    }
}