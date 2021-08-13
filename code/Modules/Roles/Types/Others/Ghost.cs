﻿using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Roles.Types.Others
{
    public class Ghost : Role
    {
        private const float RenderingAlpha = 0.25F;
        private static readonly Color32 RenderingColor = new Color32(255, 255, 255);

        public override string Id => "ghost";
        public override string Name => "Ghost";
        public override string Description => "Ghost";
        public override string Category => "ghost";

        public override IEnumerable<string> Clothing => new HashSet<string>();

        public override void OnSpawn(MainPlayer player)
        {
            base.OnSpawn(player);

            player.Transmit = TransmitType.Owner;
            player.Camera = new ThirdPersonCamera();
            player.RenderAlpha = RenderingAlpha;
            player.RenderColor = RenderingColor;
            player.RemoveCollisionLayer(CollisionLayer.PhysicsProp);
            player.RemoveCollisionLayer(CollisionLayer.Player);
        }
    }
}