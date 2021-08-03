using System;
using System.Collections.Generic;
using Sandbox;

namespace ssl.Items.Data
{
    /// <summary>
    /// Stores all references of item archetypes.
    /// </summary>
    public class ItemRegistry
    {
        private readonly Dictionary<string, Item> registry;

        public ItemRegistry()
        {
            registry = new Dictionary<string, Item>();
            RegisterItems();
        }

        public void Add(Item item)
        {
            registry.Add(item.Id, item);
        }

        public Item GetItemById(string id)
        {
            return ContainsItem(id) ? registry[id] : null;
        }

        /// <summary>
        /// Checks if the specified id is registered
        /// </summary>
        public bool ContainsItem(string id)
        {
            return registry.ContainsKey(id);
        }

        /// <summary>
        /// Register all the game items in the registry.
        /// In the future, this should disappear and should load items from an XML / JSON file.
        /// </summary>
        private void RegisterItems()
        {
            Add(new ItemFood("food.apple", "Apple", 10,
                "models/rust_props/small_junk/apple.vmdl")); //Item registration Example
            Add(new Weapon.ItemWeapon("weapon.pistol", "Pistol",
                "weapons/rust_pistol/rust_pistol.vmdl")); //Item registration Example
            Add(new ItemFood("food.wine", "Wine", 5,
                "models/citizen_props/wineglass02/wineglass01gib01_lod01.vmdl")); //Item registration Example
            Add(new ItemFood("food.hotdog", "HotDog", 5,
                "models/citizen_props/hotdog01.vmdl")); //Item registration Example
			Add(new Weapon.ItemWeapon("weapon.knife", "Knife",
				"models/knife/knife.vmdl")); //Item registration Example
			Add(new ItemFood("food.banana", "Banana", 10,
				"models/food/banana/banana.vmdl")); //Item registration Example
		}
    }
}
