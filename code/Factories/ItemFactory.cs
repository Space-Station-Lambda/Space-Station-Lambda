using System;
using ssl.Dao;
using ssl.Data;
using ssl.Modules.Items.Instances;

namespace ssl.Factories;

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

		Item item = itemData switch
		{
			ItemFoodData itemFoodData => new ItemFood {FeedingValue = itemFoodData.FeedingValue},
			ItemWeaponData itemWeaponData => new ItemWeapon
			{
				Damage = itemWeaponData.Damage,
				Range = itemWeaponData.Range,
				PrimaryRate = itemWeaponData.PrimaryRate,
				ShootSound = itemWeaponData.ShootSound,
				MuzzleFlashParticle = itemWeaponData.MuzzleFlashParticle
			},
			ItemCleanerData itemCleanerData => new ItemCleaner {CleaningValue = itemCleanerData.CleaningValue},

			_ => throw new ArgumentException("Item type not supported")
		};

		item.Id = itemData.Id;
		item.Name = itemData.Name;
		item.Model = itemData.Model;
		item.HoldType = itemData.HoldType;
		item.WasteId = itemData.WasteId;
		return item;
	}
	// Weird way to create an item. I let this here for future reference.
	/*
	public Item Create2(string id)
	{
		ItemData itemData = itemDao.FindById(id);
		Item item = new()
		{
			Id = itemData.Id,
			Name = itemData.Name,
			Model = itemData.Model,
			HoldType = itemData.HoldType,
			WasteId = itemData.WasteId
		};
		((ItemFood)item).FeedingValue = itemData switch
		{
			ItemFoodData itemFoodData => itemFoodData.FeedingValue,
			_ => throw new ArgumentException("Item type not supported")
		};
		return item;
	}
	*/
}
