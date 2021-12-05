﻿using ssl.Modules.Selection;
using ssl.Modules.Statuses.Types;
using ssl.Player;

namespace ssl.Modules.Items.Instances;

/// <summary>
///     A key is used to open or close stuff. All keys with the same colors opens the same things.
///     A key can be unique and not impacted by the procedural generator.
/// </summary>
public class ItemKey : Item
{
	/// <summary>
	///     The key is the code used to open things. The key can be set by the procedural generator.
	/// </summary>
	public string KeyCode { get; set; } = "";

	public override void OnUsePrimary( SslPlayer sslPlayer, ISelectable target )
	{
		base.OnUsePrimary(sslPlayer, target);
		if ( target is SslPlayer targetPlayer )
		{
			// Find the restrained status
			Restrained restrained = targetPlayer.StatusHandler.GetStatus<Restrained>();
			if ( null != restrained )
			{
				// Check if a restrain is here and try his lock
				if ( null != restrained.Restrain && !restrained.Restrain.Lock.Open(KeyCode) )
				{
					return;
				}

				// Resolve the restrain
				targetPlayer.StatusHandler.ResolveStatus<Restrained>();
			}
		}
	}
}