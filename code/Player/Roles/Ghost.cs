using System.Collections.Generic;
using Sandbox;

namespace ssl.Player.Roles
{
    public class Ghost : Role
    {
        private const float RenderingAlpha = 0.25F;
        
        public override string Id => "ghost";
        public override string Name => "Ghost";
        public override string Description => "Ghost";

        public override IEnumerable<string> Clothing =>  new HashSet<string>();

        public override void OnSpawn(MainPlayer player)
        {
            base.OnSpawn(player);
            
            player.Transmit = TransmitType.Owner;
            player.Camera = new ThirdPersonCamera();
            player.RenderAlpha = RenderingAlpha;
            player.RemoveCollisionLayer(CollisionLayer.PhysicsProp);
            player.RemoveCollisionLayer(CollisionLayer.Player);
        }
    }
}