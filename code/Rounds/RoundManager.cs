using Sandbox;

namespace ssl.Rounds
{
    public partial class RoundManager : NetworkComponent
    {
        //[Net] public BaseRound CurrentRound { get; private set; }
        [Net] public TestVal Val { get; set; } = new();
        public RoundManager()
        {
            // ChangeRound(new PreRound());
            
        }

        // private void ChangeRound(BaseRound round)
        // {
        //     CurrentRound?.Finish();
        //     CurrentRound = round;
        //     CurrentRound?.Start();
        // }
    }
    
    
}