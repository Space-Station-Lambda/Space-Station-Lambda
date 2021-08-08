using System;
using Sandbox;
using Sandbox.UI;

namespace ssl.UI
{
    [Library]
    public partial class Hud : HudEntity<RootPanel>
    {
        public Hud()
        {
            if (!IsClient) return;
            RootPanel.AddChild<RoundInfos>();
            RootPanel.AddChild<InventoryBar>();
            RootPanel.AddChild<Crosshair>();
            RootPanel.AddChild<RoleSelector>();
        }
    }
}