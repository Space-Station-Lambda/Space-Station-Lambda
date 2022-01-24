using System;
using ssl.Modules.Selection;
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

	public override void OnInteract(SslPlayer sslPlayer, int strength)
	{
		Restrained restrained = sslPlayer.StatusHandler.GetStatus<Restrained>();
		if (restrained != null)
		{
			ItemRestrain itemRestrain = restrained.Restrain;
			if (itemRestrain?.Lock.Open(KeyCode) != true) return;
			
			sslPlayer.StatusHandler.ResolveStatus<Restrained>();
			sslPlayer.Inventory.Add(itemRestrain);
			
			Delete();
		}
		else
		{
			base.OnInteract(sslPlayer, strength);
		}
	}

	public override void OnPressedUsePrimary(SslPlayer sslPlayer, ISelectable target)
    {
        base.OnDownUsePrimary(sslPlayer, target);
        
        if (target is not SslPlayer targetPlayer) return;

        // Find the restrained status
        Restrained restrained = targetPlayer.StatusHandler.GetStatus<Restrained>();
        
        // Check if a restrain is here and try his lock
        ItemRestrain itemRestrain = restrained?.Restrain;
        if (itemRestrain?.Lock.Open(KeyCode) != true) return;
        
        sslPlayer.Inventory.Add(itemRestrain);
        itemRestrain.Key = null;
        // Resolve the restrain
        targetPlayer.StatusHandler.ResolveStatus<Restrained>();

        Delete();
    }
}