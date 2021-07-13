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
    public class RoleSelector : Panel
    {
        private readonly List<RoleIcon> roleSlots = new();
        private int currentSelected;
        
        public RoleSelector()
        {
            StyleSheet.Load( "UI/RoleSelector.scss" );
            SetClass("active", true);
            roleSlots.Add(new RoleIcon(new Assistant(), this));
            roleSlots.Add(new RoleIcon(new Scientist(), this));
            roleSlots.Add(new RoleIcon(new Janitor(), this));
            for (int i = 0; i < roleSlots.Count; i++)
            {
                int slotToSelect = i;
                roleSlots[i].AddEventListener("onclick", () =>
                {
                    Select(slotToSelect);
                });
            }
        }
        
        /// <summary>
        /// Select a specific role
        /// </summary>
        /// <param name="slot">Slot to select</param>
        public void Select(int slot)
        {
            roleSlots[currentSelected].Unselect();
            currentSelected = slot;
            roleSlots[currentSelected].Select();
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
                ((MainPlayer) Local.Client.Pawn).SetRole(Role); //TODO improve the methode for retrieve the client with the UI.
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
