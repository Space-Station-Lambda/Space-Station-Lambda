using System;
using System.Collections.Generic;
using Sandbox;

namespace ssl.Player.Cameras
{
    public partial class SpectatorCamera : Camera
    {
        private const float SpectatorFieldOfView = 80F;
        private const float FocusDistance = 100F;
        private const float MaxSpeed = 75F;
        private const float SpeedChangeFactor = 3F;

        private bool clearTargetPressed;
        private int playerIndex;

        private IList<MainPlayer> Players => Gamemode.Instance.RoundManager.CurrentRound.Players;
        public float MoveSpeed { get; private set; } = 50F;
        public Entity Target { get; private set; }


        public override void Activated()
        {
            base.Activated();

            Target = null;
            FieldOfView = SpectatorFieldOfView;
        }

        public override void Update()
        {
            if (Target != null)
            {
                Pos = Target.Position + Input.Rotation.Backward * FocusDistance;
                Rot = Input.Rotation;

                if (Input.Pressed(InputButton.Jump))
                {
                    Target = null;
                    clearTargetPressed = true;
                }
            }
            else
            {
                float up = 0;

                if (!clearTargetPressed)
                {
                    if (Input.Down(InputButton.Jump))
                    {
                        up = 1;
                    }
                    else if (Input.Down(InputButton.Duck))
                    {
                        up = -1;
                    }
                } 
                else if (Input.Released(InputButton.Jump))
                {
                    clearTargetPressed = false;
                }
                
                MoveSpeed += Input.MouseWheel * SpeedChangeFactor;
                MoveSpeed = MoveSpeed.Clamp(0, MaxSpeed);
                
                Vector3 wishDir = new Vector3(Input.Forward, Input.Left, 0).Normal * Rot;
                wishDir += Vector3.Up * up;
                
                Pos += wishDir * MoveSpeed;
                Rot = Input.Rotation;
            }

            if (Input.Pressed(InputButton.Attack1))
            {
                PreviousTarget();
            }
            else if (Input.Pressed(InputButton.Attack2))
            {
                NextTarget();
            }
        }

        private void NextTarget()
        {

            playerIndex = Players.Count > 0 ? (playerIndex + 1) % Players.Count : -1;
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
            {
                Target = Players[playerIndex];
                Log.Trace(Target);
            }
        }
    }
}