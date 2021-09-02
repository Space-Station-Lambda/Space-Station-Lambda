namespace ssl.Modules.Rounds
{
    public class ResultsRound : BaseRound
    {
        public override string RoundName => "Results round";
        public override int RoundDuration => 10;
        
        public override BaseRound Next()
        {
            return new PreRound();
        }
    }
}