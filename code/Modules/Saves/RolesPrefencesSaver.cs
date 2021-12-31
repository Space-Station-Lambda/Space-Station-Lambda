using System.Collections.Generic;
using ssl.Modules.Roles;

namespace ssl.Modules.Saves;

public class RolesPrefencesSaver : Saver<IDictionary<string, RolePreferenceType>>
{
    private const string ROLE_PREFERENCES = "RolePreferences";

    public RolesPrefencesSaver() : base(ROLE_PREFERENCES) { }

    public new List<(string, RolePreferenceType)> Load()
    {
        IDictionary<string, RolePreferenceType> stringPreferences = base.Load();
        List<(string, RolePreferenceType)> rolePreferences = new();
        foreach ((string roleString, RolePreferenceType preference) in stringPreferences)
        {
            (string, RolePreferenceType) rolePreference = (roleString, preference);
            rolePreferences.Add(rolePreference);
        }

        return rolePreferences;
    }
}