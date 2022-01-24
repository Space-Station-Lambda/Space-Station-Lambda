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
    public ItemKey Key { get; set; }

    public override void OnPressedUsePrimary(SslPlayer sslPlayer, ISelectable target)
    {
        base.OnPressedUsePrimary(sslPlayer, target); 
        Host.AssertServer();
                                                            
        // Get the player selected and restrain him
        if (target is not SslPlayer targetPlayer || Key.IsValid()) return;
        
        AttachToPlayer(sslPlayer, targetPlayer);

        Key = (ItemKey) ItemFactory.Instance.Create(Identifiers.HANDCUFFS_KEY_ID);
        Key.KeyCode = Lock.Key;
        sslPlayer.Inventory.Add(Key);
    }

    public void AttachToPlayer(SslPlayer holder, SslPlayer target)
    {        
        holder.Inventory.RemoveItem(this);
        OnCarryStart(target);
        EnableDrawing = true;

        Restrained handcuffedStatus = (Restrained) StatusFactory.Instance.Create(Identifiers.RESTRAINED_ID);
        handcuffedStatus.Restrain = this;
        
        target.StatusHandler.ApplyStatus(handcuffedStatus);
    }
}