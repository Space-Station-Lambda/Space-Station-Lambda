using ssl.Constants;
using ssl.Player;

namespace ssl.Modules.Roles.Instances;

public class Traitor : Role
{
    public Role SecondaryRole { get; private set; }

    public override void OnAssigned(SslPlayer sslPlayer)
    {
        base.OnAssigned(sslPlayer);
        sslPlayer.RoleHandler.SetPreference(Identifiers.Roles.TRAITOR_ID, RolePreferenceType.Never);
        string defaultRole = Gamemode.Current.RoundManager.CurrentRound.RoleDistributor.DefaultRole;
        string secondaryRoleId =
            Gamemode.Current.RoundManager.CurrentRound.RoleDistributor.GetPreferedRole(sslPlayer).Equals("")
                ? defaultRole
                : Gamemode.Current.RoundManager.CurrentRound.RoleDistributor.GetPreferedRole(sslPlayer);
        SecondaryRole = RoleFactory.Instance.Create(secondaryRoleId);
        SecondaryRole.OnAssigned(sslPlayer);
    }

    public override void OnSpawn(SslPlayer sslPlayer)
    {
        base.OnSpawn(sslPlayer);
        SecondaryRole.OnSpawn(sslPlayer);
    }

    public override void OnUnassigned(SslPlayer sslPlayer)
    {
        base.OnUnassigned(sslPlayer);
        SecondaryRole.OnUnassigned(sslPlayer);
    }
}