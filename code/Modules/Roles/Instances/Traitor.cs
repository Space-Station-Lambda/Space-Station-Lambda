using System.Collections.Generic;
using ssl.Player;

namespace ssl.Modules.Roles.Types.Antagonists;

public class Traitor : Role
{
	public Role SecondaryRole { get; private set; }

	public override void OnAssigned( SslPlayer sslPlayer )
	{
		base.OnAssigned(sslPlayer);
		sslPlayer.RoleHandler.SetPreference($"{Identifiers.Role}{Identifiers.Separator}{Identifiers.Traitor}", RolePreferenceType.Never);
		string defaultRole = Gamemode.Instance.RoundManager.CurrentRound.RoleDistributor.DefaultRole;
		string secondaryRoleId = Gamemode.Instance.RoundManager.CurrentRound.RoleDistributor.GetPreferedRole(sslPlayer).Equals("") ? defaultRole : Gamemode.Instance.RoundManager.CurrentRound.RoleDistributor.GetPreferedRole(sslPlayer);
		SecondaryRole = RoleFactory.Instance.Create(secondaryRoleId);
		SecondaryRole.OnAssigned(sslPlayer);
	}

	public override void OnSpawn( SslPlayer sslPlayer )
	{
		base.OnSpawn(sslPlayer);
		SecondaryRole.OnSpawn(sslPlayer);
	}

	public override void OnUnassigned( SslPlayer sslPlayer )
	{
		base.OnUnassigned(sslPlayer);
		SecondaryRole.OnUnassigned(sslPlayer);
	}
}
