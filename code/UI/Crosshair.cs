using Sandbox.UI;

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
                //TODO modify this when something is selected; Maybe use tick()
                SetClass("selected", true);
            }
        }
    }
}