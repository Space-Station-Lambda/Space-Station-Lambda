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
        
        [Net, Predicted] public Entity Target { get; private set; }
        private List<MainPlayer> players => Gamemode.Instance.RoundManager.CurrentRound.Players;
        
        public override void Activated()
        {
            base.Activated();

            if (players.Count > 0) Target = players[playerIndex];
        }

        public override void Update()
        {
            if (Target != null)
            {
                Pos = Target.Position + Input.Rotation.Backward * FocusDistance;
                Rot = Local.Pawn.EyeRot;
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
                Target = Gamemode.Instance.RoundManager.CurrentRound.Players[playerIndex];

            FieldOfView = SpectatorFieldOfView;
        }
    }
}