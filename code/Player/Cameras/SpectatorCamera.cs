using System;
using System.Collections.Generic;
using Sandbox;

namespace ssl.Player.Cameras
{
    public partial class SpectatorCamera : Camera
    {
        private const float SpectatorFieldOfView = 80F;
        private const float FocusDistance = 100F;

        private int playerIndex;

        private List<MainPlayer> players => Gamemode.Instance.RoundManager.CurrentRound.Players;
        [Net, Predicted] public float MoveSpeed { get; private set; } = 50F;
        [Net, Predicted] public Entity Target { get; private set; }
        
        public override void Activated()
        {
            base.Activated();
        }

        public override void Update()
        {
            if (Target != null)
            {
                Pos = Target.Position + Input.Rotation.Backward * FocusDistance;
                Rot = Target.EyeRot;
            }
            else
            {
                float upVector = 0;
                if (Input.Pressed(InputButton.Jump))
                {
                    upVector = 1;
                }
                else if (Input.Pressed(InputButton.Duck))
                {
                    upVector = -1;
                }

                Vector3 wishDir = new Vector3(Input.Forward, Input.Left, upVector).Normal * Rot;
                Pos += wishDir * MoveSpeed;
                Rot = Input.Rotation;
            }

            int playerCount = Gamemode.Instance.RoundManager.CurrentRound.Players.Count;
            
            if (Input.Pressed(InputButton.Attack1))
            {
                playerIndex = playerIndex - 1 < 0 ? playerCount - 1 : playerIndex - 1;
            }
            else if (Input.Pressed(InputButton.Attack2))
            {
                playerIndex = (playerIndex + 1) % playerCount;
            }

            if (0 <= playerIndex && playerIndex < playerCount) 
                Target = players[playerIndex];

            FieldOfView = SpectatorFieldOfView;
        }
    }
}