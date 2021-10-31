using Sandbox;

namespace ssl.Modules.Roles
{
    public partial class RolePreference : BaseNetworkable
    {
        [Net] public Role Role { get; set; }
        [Net] public RolePreferenceType Preference { get; set; }

        public RolePreference()
        {
        }
        
        public RolePreference(Role role, RolePreferenceType preference)
        {
            Preference = preference;
            Role = role;
        }

        public override string ToString()
        {
            return $"{Role} - {Preference}";
        }
    }
}