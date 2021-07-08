using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using ssl.Player.Roles;

namespace ssl.UI
{
    public class RoleList : Panel
    {
        private List<RoleIcon> roleSlots = new();

        public RoleList()
        {
            StyleSheet.Load( "ui/rolelist.scss" );
            roleSlots.Add(new RoleIcon(new Assistant(), this));
            roleSlots.Add(new RoleIcon(new Scientist(), this));
        }
    }
}