using System;
using Sandbox;

namespace ssl.Player.Cameras
{
    public partial class SpectatorCamera : Sandbox.Camera
    {
        [Net, Predicted] public Entity Target { get; set; } 
        
        public override void Activated()
        {
            base.Activated();
        }

        public override void Update()
        {
            Pos = Local.Pawn.Position + Vector3.Up * 100;
            Rot = Local.Pawn.EyeRot;
        }
    }
}