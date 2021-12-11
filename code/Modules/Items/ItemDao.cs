using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items;

public class ItemDao : LocalDao<ItemData>
{
	private static ItemDao instance;

	private ItemDao()
	{
	}
	
	public static ItemDao Instance => instance ??= new ItemDao();

	/// <summary>
	///     Load all data from disk files.
	/// </summary>
	protected override void LoadAll()
	{
		Log.Info("Load items..");

		// Items
		Save(new ItemData(Identifiers.CLEANING_SPRAY_ID)
		{
			Name = "Cleaning Spray",
			Description = "A spray to clean stuff"
		});
		Save(new ItemData(Identifiers.FLASHLIGHT_ID)
		{
			Name = "Flashlight",
			Description = "A flashlight, close your eyes"
		});
		
		Save(new ItemData(Identifiers.BIN_BAG_ID)
		{
			Name = "Bin Bag",
			Description = "Collect your best trash here!"
		});

		// Foods
		Save(new ItemFoodData(Identifiers.BANANA_ID)
		{
			Name = "Banana",
			Description = "A Banana",
			FeedingValue = 10
		});
		Save(new ItemFoodData(Identifiers.APPLE_ID)
		{
			FeedingValue = 9
		});

		// Weapons
		Save(new ItemWeaponData(Identifiers.PISTOL_ID)
		{
			Name = "Pistol",
			Description = "A pistol",
			Model = "weapons/rust_pistol/rust_pistol.vmdl",
			HoldType = HoldType.Hand,
			PrimaryRate = 5.0f,
			Damage = 10.0f,
			Range = 0.0f, // Infinite
			ShootSound = "rust_pistol.shoot",
			MuzzleFlashParticle = "particles/pistol_muzzleflash.vpcf"
		});

		// Cleaners
		Save(new ItemCleanerData(Identifiers.SPONGE_ID)
		{
			Name = "Sponge",
			Description = "A sponge",
			CleaningValue = 10
		});
		
		Save(new ItemCleanerData(Identifiers.MOP_ID)
		{
			Name = "Mop",
			Description = "A mop ?",
			CleaningValue = 15
		});


		Log.Info($"{All.Count} items charged !");
	}
}
