using System;

namespace SSL_Core.model
{
    public abstract class Item
    {
        public String Id { get; } // APPLE
        public String Name { get; } // Apple
        public int MaxStack { get;  }
        public Item(String id, String name, int maxStack = 1)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
        }
    }
}