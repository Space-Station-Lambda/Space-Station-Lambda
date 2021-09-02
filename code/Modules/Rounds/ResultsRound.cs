namespace ssl.Modules.Rounds
{
    public class ResultsRound : BaseRound
    {
        public string WinningTeam { get; private set; }
        public override string RoundName => "Results round";
        public override int RoundDuration => 10;
        
        
        public ResultsRound(string winningTeam) : base()
        {
            WinningTeam = winningTeam;
        }
        
        public override BaseRound Next()
        {
            return new PreRound();
        }
    }
}