namespace ssl.Modules.Items.Data;

public class ItemWeaponData : ItemData
{
	public ItemWeaponData( string id ) : base(id)
	{
	}

	public float PrimaryRate { get; set; } = 5F;
	public float Damage { get; set; } = 10F;
	public float Range { get; set; } = 0f;
	public int MaxAmmo { get; set; } = -1;
	public float ReloadTime { get; set; } = 0F;
	public string ShootSound { get; set; } = "";
	public string MuzzleFlashParticle { get; set; } = "";
	
}
