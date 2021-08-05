using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Items.Data
{
    public abstract class Item : BaseCarriable
    {
        public static Dictionary<string, ItemData> All = new()
        {
            { 
                "food.apple", new ItemFoodData("food.apple", "Apple", "models/rust_props/small_junk/apple.vmdl",10) 
            },
            {
                "weapon.pistol", new ItemWeaponData("weapon.pistol", "Pistol", "weapons/rust_pistol/rust_pistol.vmdl", 0.5f)
            },
            {
                "food.wine", new ItemFoodData("food.wine", "Wine", "models/citizen_props/wineglass02/wineglass01gib01_lod01.vmdl", 5)
            },
            {
                "food.hotdog", new ItemFoodData("food.hotdog", "HotDog", "models/citizen_props/hotdog01.vmdl", 5)
            },
            {
                "weapon.knife", new ItemWeaponData("weapon.knife", "Knife", "models/knife/knife.vmdl", 0.5f)
            },
            {
                "food.banana", new ItemFoodData("food.banana", "Banana", "models/food/banana/banana.vmdl", 10)
            }
        };
        
        protected Item(ItemData data)
        {
            Id = data.Id;
            Name = data.Name;
            Model = data.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }

        /// <summary>
        /// Called each tick when an ItemStack of this Item is active.
        /// </summary>
        public virtual void UsedBy(MainPlayer player) { }
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