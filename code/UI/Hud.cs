using System;
using Sandbox;
using Sandbox.UI;

namespace ssl.UI
{
    [UseTemplate]
    public class Hud : HudEntity<RootPanel>
    {
        public Hud()
        {
            if (IsServer)
            {
                Log.Info("SERVER UI");
            }
            if (IsClient)
            {
                RootPanel.AddChild<RoleSelector>();
                RootPanel.AddChild<RoundInfos>();
            }
        }
    }
}