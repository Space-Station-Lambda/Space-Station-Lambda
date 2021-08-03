using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Items.Data
{
    public abstract class Item
    {
        public static Dictionary<string, Item> All = new()
        {
            { 
                "food.apple", new ItemFood("food.apple", "Apple", 10,
                "models/rust_props/small_junk/apple.vmdl") 
            },
            {
                "weapon.pistol", new Weapon.ItemWeapon("weapon.pistol", "Pistol",
                    "weapons/rust_pistol/rust_pistol.vmdl")
            },
            {
                "food.wine", new ItemFood("food.wine", "Wine", 5,
                    "models/citizen_props/wineglass02/wineglass01gib01_lod01.vmdl")
            },
            {
                "food.hotdog", new ItemFood("food.hotdog", "HotDog", 5,
                    "models/citizen_props/hotdog01.vmdl")
            },
            {
                "weapon.knife", new Weapon.ItemWeapon("weapon.knife", "Knife",
                    "models/knife/knife.vmdl")
            },
            {
                "food.banana", new ItemFood("food.banana", "Banana", 10,
                    "models/food/banana/banana.vmdl")
            }
        };
        
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
        /// Called each tick when an ItemStack of this Item is active.
        /// </summary>
        public virtual void UsedBy(MainPlayer player, ItemStack itemStack) { }
        public virtual void OnSimulate(ItemStack itemStack)
        {
            if (itemStack.Parent is MainPlayer player && Input.Down(InputButton.Attack1))
            {
                UsedBy(player, itemStack);
            }
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