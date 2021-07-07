using System;
using ssl.Player;

namespace ssl.item.ItemTypes
{
    public abstract class ItemCore
    {
        public ItemCore(
            string id,
            string name,
            string type = "",
            string model = "",
            int maxStack = 1,
            bool destroyOnUse = false)
        {
            Id = id;
            Name = name;
            Type = type;
            Model = model;
            MaxStack = maxStack;
            DestroyOnUse = destroyOnUse;
        }

        public string Id { get; private set; } // APPLE
        public string Name { get; private set; } // Apple
        public int MaxStack { get; private set; } // 100 ?
        public bool DestroyOnUse { get; private set; }
        public string Type { get; private set; }
        public string Model { get; private set; }

        /// <summary>
        /// Apply the object's effects when the user is a Player
        /// TODO : implement the destroy on use
        /// </summary>
        public abstract void UsedBy(MainPlayer player);

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}