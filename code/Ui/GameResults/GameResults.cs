using System;
using System.Globalization;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Rounds;

namespace ssl.Ui.GameResults
{
    public class GameResults : Panel
    {
        private Label winningTeam;
        
        public GameResults()
        {
            StyleSheet.Load("Ui/GameResults/GameResults.scss");

            winningTeam = Add.Label(classname: "winning-team");
            
            SetClass("active", false);
            SetClass("hidden", true);
        }

        public override void Tick()
        {
            base.Tick();
            
            ResultsRound currentRound = Gamemode.Instance.RoundManager?.CurrentRound as ResultsRound;
            
            SetClass("active", null != currentRound);
            SetClass("hidden", null == currentRound);

            if (currentRound == null) return;
            
            switch (currentRound.RoundOutcome)
            {
                case RoundOutcome.Tie:
                    winningTeam.SetText("Tie");
                    break;
                case RoundOutcome.ProtagonistsWin:
                    winningTeam.SetText("The Lambda Company wins !");
                    break;
                case RoundOutcome.TraitorsWin:
                    winningTeam.SetText("The Traitors wins !");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}