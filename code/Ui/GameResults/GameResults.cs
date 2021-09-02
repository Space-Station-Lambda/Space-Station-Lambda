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
        private const string TieText = "Tie"; 
        private const string ProtagonistsText = "The Lambda Company wins !"; 
        private const string TraitorsText = "The Traitors wins !"; 
        
        private readonly Label roundOutcome;
        
        public GameResults()
        {
            StyleSheet.Load("Ui/GameResults/GameResults.scss");

            roundOutcome = Add.Label(classname: "winning-team");
            
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
                    roundOutcome.SetText(TieText);
                    break;
                case RoundOutcome.ProtagonistsWin:
                    roundOutcome.SetText(ProtagonistsText);
                    break;
                case RoundOutcome.TraitorsWin:
                    roundOutcome.SetText(TraitorsText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}