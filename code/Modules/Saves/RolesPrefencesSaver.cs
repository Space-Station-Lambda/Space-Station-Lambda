﻿using System.Collections.Generic;
using System.Linq;
using ssl.Modules.Roles;

namespace ssl.Modules.Saves;

public class RolesPrefencesSaver : Saver<Dictionary<string, RolePreferenceType>>
{
	private const string RolePreferences = "RolePreferences";

	public RolesPrefencesSaver() : base(RolePreferences)
	{
	}

	public void Save( IEnumerable<RolePreference> toSave )
	{
		var toSaveStringified = toSave.ToDictionary(r => r.Role.Id, r => r.Preference);
		base.Save(toSaveStringified);
	}

	public new List<(string, RolePreferenceType)> Load()
	{
		var stringPreferences = base.Load();
		List<(string, RolePreferenceType)> rolePreferences = new();
		foreach ( (string roleString, RolePreferenceType preference) in stringPreferences )
		{
			(string, RolePreferenceType) rolePreference = (roleString, preference);
			rolePreferences.Add(rolePreference);
		}

		return rolePreferences;
	}
}
