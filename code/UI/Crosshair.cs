using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.UI
{
    public class Crosshair : Panel
    {
        private SelectCircle selectCircle;
        public Crosshair()
        {
            StyleSheet.Load("ui/crosshair.scss");
            selectCircle = AddChild<SelectCircle>();
        }
        [Event("buildinput")]
        public void ProcessClientInput(InputBuilder input)
        {
            if (Local.Pawn is not MainPlayer player)
                return;

            if (input.Pressed(InputButton.Attack2)) player.Holding?.UseOn(player);
        }
        
        public class SelectCircle : Panel
        {
            public SelectCircle()
            {
                //TODO modify this when something is selected; Maybe use tick()
                SetClass("selected", true);
            }
        }
    }
}