namespace ssl.Rounds
{
    public class InProgressRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 100;
        
        public override BaseRound Next()
        {
            return new PreRound();
        }
    }
}