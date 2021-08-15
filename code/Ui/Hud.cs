using Sandbox;
using Sandbox.UI;

namespace ssl.Ui
{
    [Library]
    public partial class Hud : HudEntity<RootPanel>
    {
        public Hud()
        {
            if (!IsClient) return;
            RootPanel.AddChild<RoundInfos.RoundInfos>();
            RootPanel.AddChild<InventoryBar.InventoryBar>();
            RootPanel.AddChild<Crosshair.Crosshair>();
            RootPanel.AddChild<RoleSelector.RoleSelector>();
        }
    }
}