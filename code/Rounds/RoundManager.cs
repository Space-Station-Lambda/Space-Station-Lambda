using Sandbox;

namespace ssl.Rounds
{
    public partial class RoundManager : NetworkedEntityAlwaysTransmited
    {
        [Net] public BaseRound CurrentRound { get; private set; }
        
        public RoundManager()
        {
            if (Host.IsServer)
            {
                ChangeRound(new PreRound());
            }
        }
        private void ChangeRound(BaseRound round)
        {
            CurrentRound?.Finish();
            CurrentRound = round;
            CurrentRound?.Start();
        }
    }
    
    
}