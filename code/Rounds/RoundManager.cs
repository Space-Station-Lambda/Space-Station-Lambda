using Sandbox;
using ssl.UI;

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

        [Net, OnChangedCallback] public BaseRound CurrentRound { get; private set; }

        public void OnCurrentRoundChanged()
        {
            UpdateUi();
        }
        
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
                Log.Info("Round " + CurrentRound.RoundName + " ended");
            }
            CurrentRound = round;
            CurrentRound.Start();
            CurrentRound.RoundEndedEvent += OnRoundEnd;
            Log.Info("Round " + CurrentRound.RoundName + " started");
        }

        private void UpdateUi()
        {
            Gamemode.Instance.Hud.RoleSelector.SetClass("active", CurrentRound is PreRound);
            Gamemode.Instance.Hud.RoleSelector.SetClass("hidden", CurrentRound is not PreRound);
        }
    }
}