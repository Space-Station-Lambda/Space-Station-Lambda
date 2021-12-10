using System.Collections.Generic;
using System.Linq;
using ssl.Modules.Roles;

namespace ssl.Modules.Saves;

public class RolesPrefencesSaver : Saver<IDictionary<string, RolePreferenceType>>
{
	private const string RolePreferences = "RolePreferences";

	public RolesPrefencesSaver() : base(RolePreferences)
	{
	}

	public new List<(string, RolePreferenceType)> Load()
	{
		IDictionary<string, RolePreferenceType> stringPreferences = base.Load();
		List<(string, RolePreferenceType)> rolePreferences = new();
		foreach ( (string roleString, RolePreferenceType preference) in stringPreferences )
		{
			(string, RolePreferenceType) rolePreference = (roleString, preference);
			rolePreferences.Add(rolePreference);
		}

		return rolePreferences;
	}
}
