using Sandbox;

namespace ssl.Rounds
{
    public partial class RoundManager
    {
        public BaseRound CurrentRound { get; private set; }

        public RoundManager()
        {
            ChangeRound(new PreRound());
        }

        private void ChangeRound(BaseRound round)
        {
            CurrentRound?.Finish();
            CurrentRound = round;
            CurrentRound?.Start();
        }
    }
    
    
}