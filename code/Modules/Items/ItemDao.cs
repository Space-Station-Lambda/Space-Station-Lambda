using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items;

public class ItemDao : LocalDao<ItemData>
{
    private static ItemDao instance;

    private ItemDao() { }

    public static ItemDao Instance => instance ??= new ItemDao();

    /// <summary>
    ///     Load all data from disk files.
    /// </summary>
    protected override void LoadAll()
    {
        Log.Info("Load items..");

        // Items
        Save(new ItemData(Identifiers.Items.FLASHLIGHT_ID)
        {
            Name = "Flashlight",
            Description = "A flashlight, close your eyes"
        });

        Save(new ItemData(Identifiers.Items.BIN_BAG_ID)
        {
            Name = "Bin Bag",
            Description = "Collect your best trash here!"
        });

        // Foods
        Save(new ItemFoodData(Identifiers.Items.BANANA_ID)
        {
            Name = "Banana",
            Description = "A Banana",
            FeedingValue = 10
        });
        Save(new ItemFoodData(Identifiers.Items.APPLE_ID)
        {
            FeedingValue = 9
        });

        // Weapons
        Save(new ItemWeaponData(Identifiers.Items.PISTOL_ID)
        {
            Name = "Pistol",
            Description = "A pistol",
            Model = "weapons/rust_pistol/rust_pistol.vmdl",
            HoldType = HoldType.Pistol,
            PrimaryRate = 5.0f,
            Damage = 10.0f,
            Range = 0.0f, // Infinite
            MaxAmmo = 6,
            ReloadTime = 2F,
            ShootSound = "rust_pistol.shoot",
            DryFireSound = "pistol.dryfire",
            ReloadSound = "pistol.reload",
            MuzzleFlashParticle = "particles/pistol_muzzleflash.vpcf"
        });
        Save(new ItemWeaponData(Identifiers.Items.TASER_ID)
        {
            Name = "Taser",
            Description = "Can be used to take down people. Hey you up there, come back here !",
            Model = "models/citizen_props/crowbar01.vmdl",
            HoldType = HoldType.Hand,
            PrimaryRate = 0.5f,
            Damage = 0.0f,
            Range = 100f,
            ShootSound = "sounds/physics/breaking/break_wood_plank.sound",
            MuzzleFlashParticle = ""
        });

        // Cleaners
        Save(new ItemData(Identifiers.Items.HANDCUFFS_ID)
        {
            Name = "Handcuffs",
            Description = "Don't be a bad boy next time",
            Model = "models/citizen_props/foamhand.vmdl"
        });
        
        Save(new ItemData(Identifiers.Items.HANDCUFFS_KEY_ID)
        {
            Name = "Handcuffs key",
            Model = "models/citizen_props/hotdog01.vmdl"
        });

        Log.Info($"{All.Count} items charged !");
    }
}