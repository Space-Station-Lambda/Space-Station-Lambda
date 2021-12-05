using Sandbox;

namespace ssl.Modules.Roles;

public partial class RolePreference : BaseNetworkable
{
	public RolePreference()
	{
	}

	public RolePreference( Role role, RolePreferenceType preference )
	{
		Preference = preference;
		Role = role;
	}

	[Net] public Role Role { get; set; }
	[Net] public RolePreferenceType Preference { get; set; }

	public override string ToString()
	{
		return $"{Role} - {Preference}";
	}
}
