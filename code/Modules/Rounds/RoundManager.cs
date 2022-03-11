using Sandbox;

namespace ssl.Modules.Rounds;

public partial class RoundManager : BaseNetworkable
{
    private BaseRound pendingRound;
    
    public RoundManager()
    {
        if (Host.IsServer) ChangeRound(new PreRound(1));
    }

    [Net] public BaseRound CurrentRound { get; private set; }

    public void ChangeRound(BaseRound round)
    {
        Host.AssertServer();

        Log.Info($"Change round {round.RoundName}");
        
        if (round.CanStart())
        {
            if (pendingRound != null) pendingRound.AllRequirementFulfilled -= ModifyRound;
            pendingRound = null;
            ModifyRound(round);
        }
        else if(pendingRound == null)
        {
            Log.Info($"Round {round.RoundName} cannot start now, waiting for requirements...");
            round.AllRequirementFulfilled += ModifyRound;
            pendingRound = round;
        }
    }

    private void ModifyRound(BaseRound round)
    {
        if (CurrentRound != null)
        {
            CurrentRound.Stop();
            CurrentRound.RoundEndedEvent -= OnRoundEnd;
            Gamemode.Current.PlayerKilled -= CurrentRound.OnPlayerKilled;
            Log.Info($"Round {CurrentRound.RoundName}  ended");
        }
    
        CurrentRound = round;
        CurrentRound.Start();
        CurrentRound.RoundEndedEvent += OnRoundEnd;
        Gamemode.Current.PlayerKilled += CurrentRound.OnPlayerKilled;
        Log.Info($"Round {CurrentRound.RoundName} started");
    }

    private void OnRoundEnd(BaseRound round)
    {
        ChangeRound(round.Next());
    }
}