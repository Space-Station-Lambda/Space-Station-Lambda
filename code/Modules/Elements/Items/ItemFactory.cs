using System;
using ssl.Modules.Elements.Items.Carriables;
using ssl.Modules.Elements.Items.Data;

namespace ssl.Modules.Elements.Items
{
    [Obsolete("Use ItemDao instead.")]
    public class ItemFactory : InstanceFactory<ItemData, Item>
    {
        private const string FoodPrefix = "food";
        private const string WeaponPrefix = "weapon";
        private const string ItemPrefix = "item";

        private const string FlashlightName = "flashlight";
        private const string TrashBagName = "trashbag";
        private const string MopName = "mop";
        protected override string BasePath => "data/items";

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
                        TrashBagName => new ItemTrashBag(itemData),
                        FlashlightName => new ItemFlashlight(itemData),
                        _ => new Item(itemData)
                    };
                default:
                    throw new ArgumentOutOfRangeException($"The prefix does not exist for {prefix}.{name}");
            }
        }
    }
}