using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.Ui.Crosshair
{
    public class CrosshairSelectionCircle : Panel
    {
        public CrosshairSelectionCircle()
        {
            StyleSheet.Load("Ui/Crosshair/CrosshairSelectionCircle.scss");
            SetClass("selected", false);
        }
            
        public override void Tick()
        {
            if (Local.Pawn is not MainPlayer player)
                return;

            SetClass("selected", player.Selector.IsSelected());
        }
    }
}