using System.Linq;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Roles;
using ssl.Player;

namespace ssl.Ui.RoleSelector
{
    /// <summary>
    /// Role icon to be chosed
    /// </summary>
    public class RoleSlot : Panel
    {
        private RolePreferenceType currentSelected;
        private readonly Role role;

        public RoleSlot(Role role, Panel parent) : this(role)
        {
            StyleSheet.Load("Ui/RoleSelector/RoleSlot.scss");
            Parent = parent;
            AddEventListener("onclick", Select);
            Log.Info("new");
        }
        
        public void Refresh()
        {
            RolePreferenceType newPreferenceType = ((MainPlayer)Local.Pawn).RoleHandler.GetPreference(role);
            if (currentSelected == newPreferenceType) return;
            currentSelected = newPreferenceType;
            SetClass("selected", currentSelected == RolePreferenceType.Medium);
        }

        public RoleSlot(Role role)
        {
            StyleSheet.Load("Ui/RoleSelector/RoleSelectorSlot.scss");
            this.role = role;
            Add.Label(role.Name, "role-name");
        }

        /// <summary>
        /// Select the role and setrole to the client
        /// </summary>
        public void Select()
        {
            ConsoleSystem.Run("select_preference_role", role.Id, currentSelected == RolePreferenceType.Medium ? RolePreferenceType.Never: RolePreferenceType.Medium);
        }

        
    }
}