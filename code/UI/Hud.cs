using Sandbox;
using Sandbox.UI;

namespace ssl.UI
{
    public partial class Hud : HudEntity<RootPanel>
    {
        private const string Path = "/UI/Hud.html";

        public Hud()
        {
            if (IsClient)
            {
                RootPanel.SetTemplate(Path);
            }
        }
    }
}