using System;
using ssl.Player;

namespace ssl.Items.Data
{
    public abstract class Item
    {
        public abstract string Id { get; protected set; } 
        public abstract string Name { get; protected set; }
        public virtual string Model => ""; //Find default model
        public virtual int MaxStack => 99; //Default max stack 
        public virtual bool DestroyOnUse => false;

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