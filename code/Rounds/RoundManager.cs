using Sandbox;

namespace ssl.Rounds
{
    public delegate void RoundEndedEvent(BaseRound round);
    public partial class RoundManager : NetworkedEntityAlwaysTransmitted
    {
        public RoundManager()
        {
            if (Host.IsServer)
            {
                ChangeRound(new PreRound());
            }
        }

        [Net] public BaseRound CurrentRound { get; private set; }

        public void OnRoundEnd(BaseRound round)
        {
            ChangeRound(round.Next());
        }
        
        public void ChangeRound(BaseRound round)
        {
            if (CurrentRound != null)
            {
                CurrentRound.Finish();
                CurrentRound.RoundEndedEvent -= OnRoundEnd;
                Log.Info("Round " + round.RoundName + " ended");
            }
            CurrentRound = round;
            CurrentRound.Start();
            CurrentRound.RoundEndedEvent += OnRoundEnd;
            Log.Info("Round " + round.RoundName + " started");
        }
    }
}