using System;
using Sandbox;
using ssl.Player;

namespace ssl.Items.Data
{
    public abstract class Item
    {
        protected Item(string id, string name)
        {
            Id = id;
            Name = name;
        }

        protected Item(string id, string name, string model) : this(id, name)
        {
            Model = model;
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Model { get; protected set; } = ""; //Find default model
        public virtual string ViewModelPath => "";

        public virtual void OnInit(ItemStack itemStack) { }
        /// <summary>
        /// Called when the player use the item in their hand
        /// </summary>
        public virtual void UsedBy(MainPlayer player, ItemStack itemStack) { }
        public virtual void OnSimulate(MainPlayer player, ItemStack itemStack)
        {
            if (Input.Down(InputButton.Attack2)) UsedBy(player, itemStack);
        }

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