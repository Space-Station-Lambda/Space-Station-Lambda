using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.UI
{
    public class CrosshairSelectionCircle : Panel
    {
        public CrosshairSelectionCircle()
        {
            StyleSheet.Load("ui/CrosshairSelectionCircle.scss");
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