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

        private List<MainPlayer> Players => Gamemode.Instance.RoundManager.CurrentRound.Players;
        [Net, Predicted] public float MoveSpeed { get; private set; } = 50F;
        [Net, Predicted] public Entity Target { get; private set; }
        
        public override void Activated()
        {
            base.Activated();
            
            FieldOfView = SpectatorFieldOfView;
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
                float up = 0;
                if (Input.Pressed(InputButton.Jump))
                {
                    up = 1;
                }
                else if (Input.Pressed(InputButton.Duck))
                {
                    up = -1;
                }

                Vector3 wishDir = new Vector3(Input.Forward, Input.Left, up).Normal * Rot;
                Pos += wishDir * MoveSpeed;
                Rot = Input.Rotation;
            }

            if (Input.Pressed(InputButton.Attack1))
                PreviousTarget();
            else if (Input.Pressed(InputButton.Attack2))
                NextTarget();
        }

        private void NextTarget()
        {
            playerIndex = (playerIndex + 1) % Players.Count;
            UpdateTarget();
        }

        private void PreviousTarget()
        {
            playerIndex = playerIndex - 1 < 0 ? Players.Count - 1 : playerIndex - 1;
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            if (0 <= playerIndex && playerIndex < Players.Count) 
                Target = Players[playerIndex];
        }
    }
}