using System;
using Sandbox;

namespace ssl.Modules.Rounds
{
    public partial class RoundManager : NetworkedEntityAlwaysTransmitted
    {
        public RoundManager()
        {
            if (Host.IsServer)
            {
                ChangeRound(new PreRound());
            }
        }

        public event Action RoundStarted;

        [Net] public BaseRound CurrentRound { get; private set; }

        public void ChangeRound(BaseRound round)
        {
            if (CurrentRound != null)
            {
                CurrentRound.Stop();
                CurrentRound.RoundEndedEvent -= OnRoundEnd;
                Log.Info("Round " + CurrentRound.RoundName + " ended");
            }

            CurrentRound = round;
            CurrentRound.Start();
            CurrentRound.RoundEndedEvent += OnRoundEnd;
            Log.Info("Round " + CurrentRound.RoundName + " started");
        }

        private void FireRoundStartedEvent()
        {
            RoundStarted?.Invoke();
            OnRoundStarted();
        }

        private void OnRoundEnd(BaseRound round)
        {
            ChangeRound(round.Next());
        }

        [ClientRpc]
        private void OnRoundStarted()
        {
            RoundStarted?.Invoke();
        }
    }
}