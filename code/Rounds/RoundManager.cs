using Sandbox;

namespace ssl.Rounds
{
    public delegate void RoundEnd(BaseRound round);
    
    public partial class RoundManager : Networked
    {
        public BaseRound CurrentRound = new PreRound();

        public RoundManager()
        {
            CurrentRound.RoundEndedEvent += OnRoundEnd;
        }

        public void OnRoundEnd(BaseRound round)
        {
            ChangeRound(round.Next());
        }
        
        public void ChangeRound(BaseRound round)
        {
            CurrentRound.Finish();
            CurrentRound.RoundEndedEvent -= OnRoundEnd;
            CurrentRound = round;
            CurrentRound.Start();
            CurrentRound.RoundEndedEvent += OnRoundEnd;
        }
    }
    
    
}