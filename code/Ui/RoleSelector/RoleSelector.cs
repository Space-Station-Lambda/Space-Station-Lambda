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

	/// <summary>
	///     Checks if the preround menu is open.
	/// </summary>
	private bool isOpen = true;

	/// <summary>
	///     True if we are in preround. Created for close the roundselector after round started.
	/// </summary>
	private bool isPreround;

	public RoleSelector()
	{
		StyleSheet.Load("Ui/RoleSelector/RoleSelector.scss");
		SetClass("active", true);
		foreach ( (string id, Role role) in Role.All )
		{
			RoleSlot slot = new(role, this);
			roleSlots.Add(slot);
		}
	}

	public override void Tick()
	{
		base.Tick();
		RefreshSlots();
		BaseRound currentRound = Gamemode.Instance.RoundManager?.CurrentRound;
		if ( null != currentRound )
		{
			if ( currentRound is PreRound )
			{
				isPreround = true;
				isOpen = true;
				SetClass("active", isOpen);
			}
			else
			{
				// Close the menu if the preround stops
				if ( isPreround )
				{
					isPreround = false;
					isOpen = false;
				}

				SetClass("active", isOpen);
			}
		}
	}

	[Event("buildinput")]
	public void ProcessClientInput( InputBuilder input )
	{
		if ( !isPreround && input.Pressed(InputButton.Menu) )
		{
			isOpen = !isOpen;
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
