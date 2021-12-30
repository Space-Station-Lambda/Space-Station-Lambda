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
			ItemWeaponData itemWeaponData => CreateWeapon(itemWeaponData),
			ItemCleanerData itemCleanerData => new ItemCleaner
			{
				CleaningValue = itemCleanerData.CleaningValue
			},
			_ => new Item()
		};
		
		item.Id = itemData.Id;
		item.Name = itemData.Name;
		item.Description = itemData.Description;
		item.SetModel(itemData.Model);
		item.HoldType = itemData.HoldType;
		item.WasteId = itemData.WasteId;
		return item;
	}

	private static ItemWeapon CreateWeapon(ItemWeaponData itemWeaponData)
	{
		ItemWeapon itemWeapon = itemWeaponData.Id switch
		{
			Identifiers.TASER_ID => new ItemTaser(),
			_ => new ItemWeapon()
		};

		itemWeapon.Damage = itemWeaponData.Damage;
		itemWeapon.Range = itemWeaponData.Range;
		itemWeapon.PrimaryRate = itemWeaponData.PrimaryRate;
		itemWeapon.MaxAmmo = itemWeaponData.MaxAmmo;
		itemWeapon.ReloadTime = itemWeaponData.ReloadTime;
		itemWeapon.ShootSound = itemWeaponData.ShootSound;
		itemWeapon.DryFireSound = itemWeaponData.DryFireSound;
		itemWeapon.ReloadSound = itemWeaponData.ReloadSound;
		itemWeapon.MuzzleFlashParticle = itemWeaponData.MuzzleFlashParticle;

		return itemWeapon;
	}
}
