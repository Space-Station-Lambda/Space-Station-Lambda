﻿using Sandbox;
using ssl.Modules.Items.Data;
using ssl.Modules.Locking;
using ssl.Modules.Selection;
using ssl.Modules.Statuses;
using ssl.Modules.Statuses.Types;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
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
        
        public override void OnUsePrimary(MainPlayer player, ISelectable target)
        {
            base.OnUsePrimary(player, target);
            // Get the player selected and restrain him
            if (target is MainPlayer targetPlayer)
            {
                Status handcuffedStatus = new Restrained(this);
                targetPlayer.StatusHandler.ApplyStatus(handcuffedStatus);
            }
        }
    }
}