using System;
using SSL.Player;

namespace SSL.Item.items
{
    public abstract class Item
    {
        public Item(string id, string name, string type = "", int maxStack = 1, bool destroyOnUse = false)
        {
            Id = id;
            Name = name;
            Type = type;
            MaxStack = maxStack;
            DestroyOnUse = destroyOnUse;
        }

        public string Id { get; private set; } // APPLE
        public string Name { get; private set; } // Apple
        public int MaxStack { get; private set; } // 100 ?
        public bool DestroyOnUse { get; private set; }
        public String Type { get; private set; }

        /// <summary>
        /// Uses the object
        /// TODO : implement the destroy on use
        /// </summary>
        public abstract void Use(MainPlayer mainPlayer);

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}