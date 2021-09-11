using System;
using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Items.Data;

namespace ssl.Modules.Items
{
    public class ItemFactory : InstanceFactory<ItemData, Item>
    {
        private const string FoodPrefix = "food";
        private const string WeaponPrefix = "weapon";
        private const string ItemPrefix = "item";

        private const string TorchlightName = "torchlight";
        private const string MopName = "mop";
        protected override string BasePath => "base/items";

        public override Item Create(string prefix, string name)
        {
            string filePath = GetFilePath(prefix, name);
           
            switch (prefix)
            {
                case FoodPrefix:
                    return new ItemFood(TryLoad<ItemFoodData>(filePath));
                case WeaponPrefix:
                    return new ItemWeapon(TryLoad<ItemWeaponData>(filePath));
                case ItemPrefix:
                    ItemData itemData = TryLoad<ItemData>(filePath);
                    return name switch
                    {
                        TorchlightName => new ItemTorchlight(itemData),
                        _ => new Item(itemData)
                    };
                default:
                    throw new ArgumentOutOfRangeException($"The prefix does not exist for {prefix}.{name}");
            }
        }
    }
}