using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player.Roles;

namespace ssl.UI
{
    public class RoleIcon : Button
    {
        public Role Role;
        public Label Label;

        public RoleIcon(Role role, Panel parent)
        {
            StyleSheet.Load( "ui/roleicon.scss" );
            Role = role;
            Parent = parent;
            Label = Add.Label("Role", "role-name");
        }
        
        public void Clear()
        {
            Label.Text = "";
            SetClass( "active", false );
        }

        protected override void OnClick(MousePanelEvent e)
        {
            
        }
    }
}