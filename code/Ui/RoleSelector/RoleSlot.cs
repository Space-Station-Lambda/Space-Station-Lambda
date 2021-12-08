using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Roles;
using ssl.Player;

namespace ssl.Ui.RoleSelector;

/// <summary>
///     Role icon to be chosed
/// </summary>
public class RoleSlot : Panel
{
	private readonly string roleId;
	private RolePreferenceType currentSelected;

	public RoleSlot( string roleId, Panel parent ) : this(roleId)
	{
		StyleSheet.Load("Ui/RoleSelector/RoleSlot.scss");
		Parent = parent;
		AddEventListener("onclick", Select);
	}

	public RoleSlot( string roleId )
	{
		StyleSheet.Load("Ui/RoleSelector/RoleSlot.scss");
		this.roleId = roleId;
		Role role = RoleFactory.Instance.Create(roleId);
		Add.Label(role.Name, "role-name");
	}

	public void Refresh()
	{
		RolePreferenceType newPreferenceType = ((SslPlayer)Local.Pawn).RoleHandler.GetPreference(roleId);

		if ( currentSelected == newPreferenceType )
		{
			return;
		}

		currentSelected = newPreferenceType;
		SetClass("selected", currentSelected == RolePreferenceType.Medium);
	}

	/// <summary>
	///     Select the role and setrole to the client
	/// </summary>
	public void Select()
	{
		ConsoleSystem.Run("select_role_preference", roleId,
			currentSelected == RolePreferenceType.Medium ? RolePreferenceType.Never : RolePreferenceType.Medium);
		ConsoleSystem.Run("save_role_preferences");
	}
}
