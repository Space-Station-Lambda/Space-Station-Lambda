using System;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Items.Data;
using ssl.Modules.Items.Instances;

namespace ssl.Modules.Items;

public sealed class ItemFactory : IFactory<Item>
{
	private static ItemFactory instance;
	private readonly ItemDao itemDao = new();

	private ItemFactory()
	{
	}

	public static ItemFactory Instance => instance ??= new ItemFactory();

	public Item Create( string id )
	{
		ItemData itemData = itemDao.FindById(id);
		
		Item item;
		string type = itemData.GetTypeId();

		switch (type)
		{
			case Identifiers.Food:
				ItemFoodData itemFoodData = (ItemFoodData)itemData; 
				item = new ItemFood { FeedingValue = itemFoodData.FeedingValue };
				break;
			case Identifiers.Weapon:
				ItemWeaponData itemWeaponData = (ItemWeaponData)itemData; 
				item = new ItemWeapon
				{
					Damage = itemWeaponData.Damage,
					Range = itemWeaponData.Range,
					PrimaryRate = itemWeaponData.PrimaryRate,
					ShootSound = itemWeaponData.ShootSound,
					MuzzleFlashParticle = itemWeaponData.MuzzleFlashParticle
				};
				break;
			case Identifiers.Cleaner:
				ItemCleanerData itemCleanerData = (ItemCleanerData)itemData;
				item = new ItemCleaner { CleaningValue = itemCleanerData.CleaningValue };
				break;
			default:
				item = new Item();
				break;
				
		}

		item.Id = itemData.Id;
		item.Name = itemData.Name;
		item.Model = itemData.Model;
		item.HoldType = itemData.HoldType;
		item.WasteId = itemData.WasteId;
		return item;
	}
}
