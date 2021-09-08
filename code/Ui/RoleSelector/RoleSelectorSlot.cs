using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Roles;

namespace ssl.Ui.RoleSelector
{
    /// <summary>
    /// Role icon to be chosed
    /// </summary>
    public class RoleSelectorSlot : Panel
    {
        private AnimSceneObject modelObject;
        public Role Role;

        public RoleSelectorSlot(Role role, Panel parent) : this(role)
        {
            Parent = parent;
        }

        public RoleSelectorSlot(Role role)
        {
            StyleSheet.Load("Ui/RoleSelector/RoleSelectorSlot.scss");
            Role = role;

            Add.Label(role.Name, "role-name");
        }

        /// <summary>
        /// Select the role and setrole to the client
        /// </summary>
        public void Select()
        {
            SetClass("selected", true);
            ConsoleSystem.Run("select_preference_role", Role.Id, RolePreference.Medium);
        }

        /// <summary>
        /// UnSelect the the role
        /// </summary>
        public void Unselect()
        {
            SetClass("selected", false);
            ConsoleSystem.Run("select_preference_role", Role.Id, RolePreference.Never);
        }
    }
}