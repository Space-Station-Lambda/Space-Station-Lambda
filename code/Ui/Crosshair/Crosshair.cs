using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.UI
{
    public class Crosshair : Panel
    {
        private CrosshairSelectionCircle crosshairSelectionCircle;

        public Crosshair()
        {
            StyleSheet.Load("ui/crosshair.scss");
            crosshairSelectionCircle = AddChild<CrosshairSelectionCircle>();
        }
        }
    
}