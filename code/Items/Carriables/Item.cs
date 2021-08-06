using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Items.Data
{
    public partial class Item : BaseCarriable
    {

        protected Item()
        {
        }
        
        public Item(ItemData data)
        {
            Id = data.Id;
            Name = data.Name;
            Model = data.Name;
        }

        [Net] public string Id { get; set; }
        [Net] public string Name { get; set; }
        [Net] public string Model { get; set; }

        /// <summary>
        /// Called each tick when an ItemStack of this Item is active.
        /// </summary>
        public virtual void UsedOn(MainPlayer player) { }

        public override void Simulate(Client cl)
        {
            base.Simulate(cl);
            
            //TODO: Change this to use an InputHandler class
            if (Input.Down(InputButton.Attack1)) UsedOn(cl.Pawn as MainPlayer);
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
