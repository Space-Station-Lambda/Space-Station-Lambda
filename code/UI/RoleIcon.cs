using System;
using System.ComponentModel;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player.Roles;

namespace ssl.UI
{
    public class RoleIcon : Panel
    {
        public Role Role;
        public Label Label;

        public RoleIcon(Role role, Panel parent)
        {
            StyleSheet.Load( "ui/roleicon.scss" );
            Role = role;
            Parent = parent;
            Label = Add.Label(role.Name, "role-name");
        }

        public void Select()
        {
            Style.BackgroundColor = Color.Cyan;
        }
        
        public void UnSelect()
        {
            Style.BackgroundColor = null;
        }
        
    }
}