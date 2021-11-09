using ssl.Modules.Elements.Items.Data;
using ssl.Modules.Locking;
using ssl.Modules.Selection;
using ssl.Modules.Statuses;
using ssl.Modules.Statuses.Types;
using ssl.Player;

namespace ssl.Modules.Elements.Items.Carriables
{
    public class ItemRestrain : Item
    {
        public ItemRestrain()
        {
        }

        public ItemRestrain(ItemData itemData) : base(itemData)
        {
            //For now , create a basic lock
            Lock = new Lock();
        }
        
        public Lock Lock { get; }
        
        public override void OnUsePrimary(SslPlayer sslPlayer, ISelectable target)
        {
            base.OnUsePrimary(sslPlayer, target);
            // Get the player selected and restrain him
            if (target is SslPlayer targetPlayer)
            {
                Status handcuffedStatus = new Restrained(this);
                targetPlayer.StatusHandler.ApplyStatus(handcuffedStatus);
            }
        }
    }
}