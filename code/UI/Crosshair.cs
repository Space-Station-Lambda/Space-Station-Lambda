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

        public class SelectCircle : Panel
        {
            public SelectCircle()
            {
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
}