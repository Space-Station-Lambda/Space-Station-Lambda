using Sandbox.UI;

namespace ssl.Ui.Crosshair
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