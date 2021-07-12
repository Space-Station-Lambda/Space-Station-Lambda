using System;
using ssl.Player;

namespace ssl.Items.Data
{
    public abstract class Item
    {
        protected Item(string id, string name, string model, int maxStack, bool destroyOnUse)
        {
            Id = id;
            Name = name;
            Model = model;
            MaxStack = maxStack;
            DestroyOnUse = destroyOnUse;
        }

        public string Id { get; protected set; } 
        public string Name { get; protected set; }
        public string Model { get; protected set; }
        public int MaxStack { get; protected set; }
        public bool DestroyOnUse { get; protected set; }

        /// <summary>
        /// Apply the object's effects when the user is a Player
        /// TODO : implement the destroy on use
        /// </summary>
        public abstract void UsedBy(MainPlayer player);

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }

        protected bool Equals(Item other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Item)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}