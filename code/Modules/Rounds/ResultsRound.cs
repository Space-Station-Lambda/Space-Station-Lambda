using Sandbox;

namespace ssl.Modules.Rounds
{
    public partial class ResultsRound : BaseRound
    {
        public ResultsRound()
        {
        }
        
        public ResultsRound(string winningTeam)
        {
            WinningTeam = winningTeam;
        }
        
        [Net] public string WinningTeam { get; set; }
        public override string RoundName => "Results round";
        public override int RoundDuration => 10;

        public override BaseRound Next()
        {
            return new PreRound();
        }
    }
}