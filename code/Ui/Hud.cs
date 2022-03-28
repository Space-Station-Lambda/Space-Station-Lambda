using Sandbox;
using Sandbox.UI;
using ssl.Ui.PlayerHUD;

namespace ssl.Ui;

public class Hud : HudEntity<RootPanel>
{
    public Hud()
    {
        if (!IsClient) return;

        RootPanel.AddChild<RoundInfos.RoundInfos>();
        RootPanel.AddChild<NotificationHandler.NotificationHandler>();
        RootPanel.AddChild<InventoryBar.InventoryBar>();
        RootPanel.AddChild<Crosshair.Crosshair>();
        RootPanel.AddChild<RoleIndicator>();
        RootPanel.AddChild<RoleSelector.RoleSelector>();
        RootPanel.AddChild<GameResults.GameResults>();
    }
}