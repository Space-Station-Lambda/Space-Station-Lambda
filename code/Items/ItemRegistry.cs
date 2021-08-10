using System.Collections.Generic;
using ssl.Items.Data;

namespace ssl.Items
{
    /// <summary>
    /// Stores all references of item archetypes.
    /// </summary>
    public class ItemRegistry
    {
        private readonly Dictionary<string, ItemData> registry;

        public ItemRegistry()
        {
            registry = new Dictionary<string, ItemData>();
            RegisterItems();
        }

        public void Add(ItemData item)
        {
            registry.Add(item.Id, item);
        }

        public ItemData GetItemById(string id)
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
            //Food
            Add(new ItemFoodData("food.apple", "Apple", "models/rust_props/small_junk/apple.vmdl", 10));
            Add(new ItemFoodData("food.wine", "Wine", "models/citizen_props/wineglass02/wineglass01gib01_lod01.vmdl", 5));
            Add(new ItemFoodData("food.hotdog", "HotDog", "models/citizen_props/hotdog01.vmdl", 5));
            Add(new ItemFoodData("food.banana", "Banana", "models/food/banana/banana.vmdl", 10));
            //Weapons
            Add(new ItemWeaponData("weapon.pistol", "Pistol", "weapons/rust_pistol/rust_pistol.vmdl"));
            Add(new ItemWeaponData("weapon.knife", "Knife", "models/knife/knife.vmdl", damage: 5000, range: 100f));

        }
    }
}
