using System;
using Sandbox;

namespace ssl.Player.Cameras
{
    public partial class SpectatorCamera : Camera
    {
        private const float SpectatorFieldOfView = 80F;
        private const float FocusDistance = 100F;
        
        [Net, Predicted] public Entity Target { get; set; }

        public override void Update()
        {
            if (Target != null)
            {
                Pos = Target.Position + Input.Rotation.Backward * FocusDistance;
                Rot = Local.Pawn.EyeRot;
            }

            FieldOfView = SpectatorFieldOfView;
        }
    }
}