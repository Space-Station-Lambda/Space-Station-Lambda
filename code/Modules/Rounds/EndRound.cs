namespace ssl.Modules.Rounds
{
    public partial class EndRound : BaseRound
    {
        public EndRound()
        {
        }

        public EndRound(RoundOutcome outcome)
        {
            RoundOutcome = outcome;
        }

        public RoundOutcome RoundOutcome { get; private set; }
        public override string RoundName => "Results round";
        public override int RoundDuration => 10;

        public override BaseRound Next()
        {
            return new PreRound();
        }
    }
}