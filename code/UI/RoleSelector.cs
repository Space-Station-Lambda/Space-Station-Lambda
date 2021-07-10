using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;
using ssl.Player.Roles;

namespace ssl.UI
{
    public class RoleSelector : Panel
    {
        private List<RoleIcon> roleSlots = new();
        private int currentSelected = 0;
        
        public RoleSelector()
        {
            StyleSheet.Load( "UI/RoleSelector.scss" );
            roleSlots.Add(new RoleIcon(new Assistant(), this));
            roleSlots.Add(new RoleIcon(new Scientist(), this));
            for (int i = 0; i < roleSlots.Count; i++)
            {
                int slotToSelect = i;
                roleSlots[i].AddEventListener("onclick", () =>
                {
                    Select(slotToSelect);
                });
            }
        }
        
        public void Select(int slot)
        {
            roleSlots[currentSelected].Unselect();
            currentSelected = slot;
            roleSlots[currentSelected].Select();
        }
        public class RoleIcon : Panel
        {
            public Role Role;
            public SceneWorld AvatarWorld { get; set; }
            public Scene AvatarScene { get; set; }
            private AnimSceneObject modelObject;
            private readonly List<AnimSceneObject> clothingObjects = new();
            
            public RoleIcon(Role role, Panel parent)
            {
                StyleSheet.Load( "ui/roleicon.scss" );
                Role = role;
                Parent = parent;
                Add.Label(role.Name, "role-name");
            }

            public void Select()
            {
                SetClass("selected", true);
                ((MainPlayer) Local.Client.Pawn).SetRole(Role);
            }

            public void Unselect()
            {
                SetClass("selected", false);
            }
        }
    }
}