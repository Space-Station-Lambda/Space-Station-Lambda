namespace ssl.Modules.Items.Data;

public class ItemWeaponData : ItemData
{
	public ItemWeaponData( string id ) : base(id)
	{
	}

	public float PrimaryRate { get; set; } = 5f;
	public float Damage { get; set; } = 10f;
	public float Range { get; set; } = 0f;
	public string ShootSound { get; set; } = "";
	public string MuzzleFlashParticle { get; set; } = "";
}
