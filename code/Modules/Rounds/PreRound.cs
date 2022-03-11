using ssl.Modules.Rounds.Requirements;

namespace ssl.Modules.Rounds;

public class PreRound : BaseRound
{
    public override string RoundName => "Preround";
    public override int RoundDuration => 10;

    public PreRound() {}

    public PreRound(int requiredPlayerCount)
    {
        Requirements.Add(new PlayerCountRequirement(requiredPlayerCount));
        RegisterRequirementsListeners();
    }

    public override BaseRound Next()
    {
        return new InProgressRound();
    }

    protected override void OnTimeUp()
    {
        AssignRoles();
        base.OnTimeUp();
    }

    private void AssignRoles()
    {
        RoleDistributor.Distribute();
    }
}