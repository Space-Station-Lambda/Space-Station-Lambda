using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Modules.Roles;

namespace ssl.Modules.Saves
{
    public class RolesPrefencesSaver : Saver<Dictionary<string, RolePreferenceType>>
    {
        private const string RolePreferences = "RolePreferences";
        public RolesPrefencesSaver() : base(RolePreferences)
        {
        }

        public void Save(IEnumerable<RolePreference> toSave)
        {
            Dictionary<string, RolePreferenceType> toSaveStringified = toSave.ToDictionary(r => r.Role.Id, r => r.Preference);
            base.Save(toSaveStringified);
        }

        public new List<RolePreference> Load()
        {
            Dictionary<string, RolePreferenceType> stringPreferences = base.Load();
            List<RolePreference> rolePreferences = new();
            foreach ((string roleString, RolePreferenceType preference) in stringPreferences)
            {
                rolePreferences.Add(new RolePreference(Role.All[roleString], preference));
            }
            return rolePreferences;
        }
    }
}