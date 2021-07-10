using System;
using System.ComponentModel;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;
using ssl.Player.Roles;

namespace ssl.UI
{
    public class RoleIcon : Panel
    {
        public Role Role;
        public Label Label;
        public bool IsSelected;

        public RoleIcon(Role role, Panel parent)
        {
            StyleSheet.Load( "ui/roleicon.scss" );
            Role = role;
            Parent = parent;
            Label = Add.Label(role.Name, "role-name");
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