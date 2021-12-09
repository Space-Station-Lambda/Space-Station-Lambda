using System.Collections.Generic;
using ssl.Commons;
using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items;

public sealed class ItemDao : LocalDao<ItemData>
{
	public ItemDao()
	{
		LoadAll();
	}

	protected override Dictionary<string, ItemData> All { get; set; } = new();

	/// <summary>
	///     Load all data from disk files.
	/// </summary>
	protected override void LoadAll()
	{
		Log.Info("Load items..");

		// Items
		Save(new ItemData("cleaning_spray")
		{
			Name = "Cleaning Spray",
			Description = "A spray to clean stuff"
		});
		Save(new ItemData("flashlight")
		{
			Name = "Flashlight",
			Description = "A flashlight, close your eyes"
		});
		Save(new ItemData("mop")
		{
			Name = "Mop",
			Description = "A mop ?"
		});
		Save(new ItemData("sponge")
		{
			Name = "Sponge",
			Description = "A spongebob fake"
		});
		Save(new ItemData("trashbag")
		{
			Name = "Trashbag",
			Description = "Collect your best trash here!"
		});

		// Foods
		Save(new ItemFoodData("banana")
		{
			Name = "Banana",
			Description = "A Banana",
			FeedingValue = 10
		});
		Save(new ItemFoodData("apple")
		{
			FeedingValue = 9
		});

		// Weapons
		Save(new ItemWeaponData("pistol")
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
		Save(new ItemCleanerData("sponge")
		{
			Name = "Sponge",
			Description = "A sponge",
			CleaningValue = 10
		});

		Log.Info($"{All.Count} items charged !");
	}
}
