using System.Collections.Generic;
using ssl.Player;

namespace ssl.Modules.Roles.Types.Antagonists;

public class Traitor : Antagonist
{
	public override string Id => "traitor";
	public override string Name => "Traitor";
	public override string Description => "Traitor";
	public override Faction[] Faction => new[] {Roles.Faction.Traitors};

	public override IEnumerable<string> Clothing => new HashSet<string>
	{
		"models/citizen_clothes/trousers/trousers.smart.vmdl",
		"models/citizen_clothes/shoes/shoes.police.vmdl",
		"models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
		"models/citizen_clothes/hat/hat_beret.black.vmdl"
	};

	public override IEnumerable<string> Items => new List<string>();

	public Role SecondaryRole { get; set; }

	public override void OnAssigned( SslPlayer sslPlayer )
	{
		base.OnAssigned(sslPlayer);
		sslPlayer.RoleHandler.SetPreference(new Traitor(), RolePreferenceType.Never);
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
