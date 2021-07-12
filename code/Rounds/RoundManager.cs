using Sandbox;

namespace ssl.Rounds
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

        [Net] public BaseRound CurrentRound { get; private set; }

        private void ChangeRound(BaseRound round)
        {
            CurrentRound?.Finish();
            CurrentRound = round;
            CurrentRound?.Start();
        }
    }
}