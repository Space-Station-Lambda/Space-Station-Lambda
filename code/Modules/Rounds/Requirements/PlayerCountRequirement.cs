using Sandbox;
using ssl.Player;

namespace ssl.Modules.Rounds.Requirements;

public class PlayerCountRequirement : BaseRequirement
{
    public PlayerCountRequirement(int required)
    {
        RequiredCount = required;
    }
    
    public int RequiredCount { get; private set; }

    public override void RegisterListeners()
    {
        Gamemode.Current.PlayerSpawned += TriggerPlayerCountRequirement;
        Gamemode.Current.PlayerDisconnected += TriggerFulfillmentEvent;
    }

    public override void UnregisterListeners()
    {
        Gamemode.Current.PlayerSpawned -= TriggerPlayerCountRequirement;
        Gamemode.Current.PlayerDisconnected -= TriggerFulfillmentEvent;
    }

    protected override bool CheckIfFulfilled()
    {
        return Client.All.Count >= RequiredCount;
    }

    private void TriggerPlayerCountRequirement(SslPlayer _)
    {
        TriggerFulfillmentEvent();
    }
}