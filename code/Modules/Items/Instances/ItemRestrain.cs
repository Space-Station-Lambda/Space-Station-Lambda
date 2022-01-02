using Sandbox;
using ssl.Constants;
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
        if (Host.IsServer) Lock = new Lock();
    }

    public Lock Lock { get; }

    public override void OnUsePrimary(SslPlayer sslPlayer, ISelectable target)
    {
        base.OnUsePrimary(sslPlayer, target);
        
        Host.AssertServer();
        
        // Get the player selected and restrain him
        if (target is SslPlayer targetPlayer)
        {
            Restrained handcuffedStatus = (Restrained) StatusFactory.Instance.Create(Identifiers.RESTRAINED_ID);
            handcuffedStatus.Restrain = this;
            
            targetPlayer.StatusHandler.ApplyStatus(handcuffedStatus);

            ItemKey key = (ItemKey) ItemFactory.Instance.Create(Identifiers.HANDCUFFS_KEY_ID);
            key.KeyCode = Lock.Key;
            sslPlayer.Inventory.Add(key);
        }
    }
}