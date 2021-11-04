﻿using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Roles.Types.Others
{
    public class Ghost : Role
    {
        private const float RenderingAlpha = 0.25f;
        private const float BasicAlpha = 1f;

        public override string Id => "ghost";
        public override string Name => "Ghost";
        public override string Description => "Ghost";
        public override string Category => "ghost";
        
        //TODO Implement the old role for the ghost (need his old faction)
        public override Faction[] Faction => Array.Empty<Faction>(); // The ghost don't have any faction. Because you know ...

        public override IEnumerable<string> Clothing => new HashSet<string>();

        public override void OnSpawn(Player.Player player)
        {
            base.OnSpawn(player);

            player.Transmit = TransmitType.Owner;
            player.RenderAlpha = RenderingAlpha;
            player.RemoveCollisionLayer(CollisionLayer.PhysicsProp);
            player.RemoveCollisionLayer(CollisionLayer.Player);
        }

        public override void OnUnassigned(Player.Player player)
        {
            base.OnUnassigned(player);

            player.RenderAlpha = BasicAlpha;
        }
    }
}