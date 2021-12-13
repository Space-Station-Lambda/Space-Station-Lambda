using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using ssl.Modules.Roles;
using ssl.Modules.Rounds;

namespace ssl.Ui.RoleSelector;

/// <summary>
///     Role selector allow player to select a role
/// </summary>
public class RoleSelector : Panel
{
	private readonly List<RoleSlot> roleSlots = new();

	public RoleSelector()
	{
		StyleSheet.Load("Ui/RoleSelector/RoleSelector.scss");
		SetClass("active", true);
		foreach ( string id in RoleDao.Instance.FindAllIds() )
		{
			RoleSlot slot = new(id, this);
			roleSlots.Add(slot);
		}
	}

	public override void Tick()
	{
		base.Tick();
		RefreshSlots();
		BaseRound currentRound = Gamemode.Instance.RoundManager?.CurrentRound;
		switch (currentRound)
		{
			case null:
				return;
			case PreRound:
				SetClass("active", true);
				break;
			default:
				SetClass("active", false);
				break;
		}
	}


	private void RefreshSlots()
	{
		foreach ( RoleSlot slot in roleSlots )
		{
			slot.Refresh();
		}
	}
}
