using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;
using ssl.Player.Roles;

namespace ssl.UI
{
    /// <summary>
    /// Role selector allow player to select a role
    /// </summary>
    [UseTemplate]
    public class RoleSelector : Panel
    {
        private readonly Dictionary<RoleIcon, bool> rolesSelected = new();
        private int currentSelected;
        
        public RoleSelector()
        {
            SetClass("active", true);
            rolesSelected.Add(new RoleIcon(new Assistant(), this), true);
            rolesSelected.Add(new RoleIcon(new Janitor(), this), false);
            rolesSelected.Add(new RoleIcon(new Scientist(), this), false);
            rolesSelected.Add(new RoleIcon(new Guard(), this), false);
            rolesSelected.Add(new RoleIcon(new Captain(), this), false);
            rolesSelected.Add(new RoleIcon(new Mechanic(), this), false);
            rolesSelected.Add(new RoleIcon(new Engineer(), this), false);
            rolesSelected.Add(new RoleIcon(new Traitor(), this), false);
            foreach (RoleIcon roleIcon in rolesSelected.Keys)
            {
                roleIcon.AddEventListener("onclick", () =>
                {
                    Select(roleIcon);
                });
            }
        }
        
        /// <summary>
        /// Select a specific role
        /// </summary>
        /// <param name="roleIcon">Slot to select</param>
        public void Select(RoleIcon roleIcon)
        {
            if (rolesSelected[roleIcon])
            {
                roleIcon.Unselect();
                rolesSelected[roleIcon] = false;
            }
            else
            {
                roleIcon.Select();
                rolesSelected[roleIcon] = true;
            }
        }
        
        /// <summary>
        /// Role icon to be chosed
        /// </summary>
        public class RoleIcon : Panel
        {
            public Role Role;
            private AnimSceneObject modelObject;

            public RoleIcon(Role role, Panel parent)
            {
                Role = role;
                Parent = parent;
                Add.Label(role.Name, "role-name");
            }
            
            /// <summary>
            /// Select the role and setrole to the client
            /// </summary>
            public void Select()
            {
                SetClass("selected", true);
                ((MainPlayer) Local.Client.Pawn).AssignRole(Role); //TODO improve the methode for retrieve the client with the UI.
            }
            
            /// <summary>
            /// UnSelect the the role
            /// </summary>
            public void Unselect()
            {
                SetClass("selected", false);
            }
        }
    }
}
