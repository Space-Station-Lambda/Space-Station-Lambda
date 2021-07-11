namespace ssl.Rounds
{
    public class InProgressRound : BaseRound
    {
        public override BaseRound Next()
        {
            return new PreRound();
        }
    }
}