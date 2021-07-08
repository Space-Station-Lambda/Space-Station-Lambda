using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using ssl.Player.Roles;

namespace ssl.UI
{
    public class RoleList : Panel
    {
        private List<RoleIcon> roleSlots = new();
        private int currentSelected = 0;
        
        public RoleList()
        {
            StyleSheet.Load( "ui/rolelist.scss" );
            roleSlots.Add(new RoleIcon(new Assistant(), this));
            roleSlots.Add(new RoleIcon(new Scientist(), this));
        }
        
        public void Select(int slot)
        {
            roleSlots[currentSelected].UnSelect();
            currentSelected = slot;
            roleSlots[currentSelected].Select();
        }
        [Event( "buildinput" )]
        public void ProcessClientInput( InputBuilder input )
        {
            if ( Local.Pawn is not Sandbox.Player) return;

            if (input.Pressed(InputButton.Slot1))
            {
                Select(0);
            }
            if (input.Pressed(InputButton.Slot2))
            {
                Select(1);
            }
        }
    }
}