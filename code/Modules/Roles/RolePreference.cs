using Sandbox;

namespace ssl.Modules.Roles
{
    public partial class RolePreference : NetworkedEntityAlwaysTransmitted
    {
        [Net] public Role Role { get; set; }
        [Net] public RolePreferenceType Preference { get; set; }

        public RolePreference(Role role, RolePreferenceType preference)
        {
            Preference = preference;
            Role = role;
        }
    }
}