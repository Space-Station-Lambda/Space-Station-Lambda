using Sandbox;
using Sandbox.UI;

namespace ssl.UI
{
    [UseTemplate]
    public class Hud : HudEntity<RootPanel>
    {
        public Hud()
        {
            if (IsClient)
            {
                Panel roleListPanel = RootPanel.AddChild<RoleList>();
                roleListPanel.CreateEvent("onSelectRole");            
            }
        }
    }
}