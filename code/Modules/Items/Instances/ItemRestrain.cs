using ssl.Modules.Locking;
using ssl.Modules.Selection;
using ssl.Modules.Statuses;
using ssl.Modules.Statuses.Types;
using ssl.Player;

namespace ssl.Modules.Items.Instances;

public class ItemRestrain : Item
{
	public ItemRestrain()
	{
		//For now , create a basic lock
		Lock = new Lock();
	}

	public Lock Lock { get; }

	public override void OnUsePrimary( SslPlayer sslPlayer, ISelectable target )
	{
		base.OnUsePrimary(sslPlayer, target);
		// Get the player selected and restrain him
		if ( target is SslPlayer targetPlayer )
		{
			Status handcuffedStatus = new Restrained
			{
				Restrain = this
			};
			targetPlayer.StatusHandler.ApplyStatus(handcuffedStatus);
		}
	}
}
