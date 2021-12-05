using Sandbox;
using ssl.Factories;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Instances;

public partial class ItemFood : Item
{
	private const string EatSound = "grunt1";


	public int FeedingValue { get; set; }

	public string Waste { get; set; }

	/// <summary>
	///     First version, food feeds up the player on use
	/// </summary>
	public override void OnUsePrimary( SslPlayer sslPlayer, ISelectable target )
	{
		OnCarryDrop(this);
		ActiveEnd(sslPlayer, false);
		sslPlayer.Inventory.RemoveItem(this);

		if ( !string.IsNullOrWhiteSpace(Waste) )
		{
			ItemFactory factory = ItemFactory.Instance;
			Item waste = factory.Create(Waste);
			sslPlayer.Inventory.Add(waste);
		}

		PlayEatSound(sslPlayer);
		if ( Host.IsServer )
		{
			Delete();
		}
	}

	[ClientRpc]
	private void PlayEatSound( Entity entity )
	{
		Sound.FromEntity(EatSound, entity);
	}
}
