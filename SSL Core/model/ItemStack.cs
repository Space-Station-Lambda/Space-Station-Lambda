﻿using System;
using SSL_Core.exception;
using SSL_Core.model.items;

namespace SSL_Core.model
{
    public class ItemStack
    {
        public Item Data { get; }
        public int Amount { get; private set; }

        public ItemStack(Item data, int amount = 0)
        {
            Data = data;
            Amount = amount;
        }
        
        public ItemStack Remove(int number)
        {
            if (number > Amount)
            {
                throw new NegativeStackItemException();
            }
        
            Amount -= number;
            
            return new ItemStack(Data, number);
        }

        public void Add(int number)
        {
            Amount += number;
        }
    }
}