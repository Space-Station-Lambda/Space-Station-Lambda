using System;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Items.Data;
using ssl.Modules.Items.Instances;

namespace ssl.Modules.Items;

public sealed class ItemFactory : IFactory<Item>
{
	private static ItemFactory instance;

	private ItemFactory()
	{
	}

	public static ItemFactory Instance => instance ??= new ItemFactory();

	public Item Create( string id )
	{
		ItemData itemData = ItemDao.Instance.FindById(id);

		Item item = itemData switch
		{
			ItemFoodData itemFoodData => new ItemFood
			{
				FeedingValue = itemFoodData.FeedingValue
			},
			ItemWeaponData itemWeaponData => new ItemWeapon
			{
				Damage = itemWeaponData.Damage,
				Range = itemWeaponData.Range,
				PrimaryRate = itemWeaponData.PrimaryRate,
				ShootSound = itemWeaponData.ShootSound,
				MuzzleFlashParticle = itemWeaponData.MuzzleFlashParticle
			},
			ItemCleanerData itemCleanerData => new ItemCleaner
			{
				CleaningValue = itemCleanerData.CleaningValue
			},
			_ => new Item()
		};

		item.Id = itemData.Id;
		item.Name = itemData.Name;
		item.Model = itemData.Model;
		item.HoldType = itemData.HoldType;
		item.WasteId = itemData.WasteId;
		return item;
	}
}
