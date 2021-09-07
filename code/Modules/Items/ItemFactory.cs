using System;
using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Items.Data;

namespace ssl.Modules.Items
{
    public class ItemFactory
    {
        private const string FoodPrefix = "food";
        private const string WeaponPrefix = "weapon";
        private const string ItemPrefix = "item";

        private const string TorchlightName = "torchlight";
        private const string MopName = "mop";
        
        
        public static Item Create(ItemData itemData)
        {
            return Create(itemData.Id);
        }
        
        public static Item Create(string id)
        {
            string prefix = id.Split(".")[0];
            string name = id.Split(".")[1];
            string filePath = $"data/items/{prefix}/{name}.{prefix}";
            switch (prefix)
            {
                case FoodPrefix:
                    return new ItemFood(Resource.FromPath<ItemFoodData>(filePath));
                case WeaponPrefix:
                    return new ItemWeapon(Resource.FromPath<ItemWeaponData>(filePath));
                case ItemPrefix:
                    ItemData itemData = Resource.FromPath<ItemData>(filePath);
                    return name switch
                    {
                        TorchlightName => new ItemTorchlight(itemData),
                        MopName => new ItemMop(itemData),
                        _ => new Item(itemData)
                    };
                default:
                    throw new ArgumentOutOfRangeException($"The prefix does not exist for {id}");
            }
        }
    }
}