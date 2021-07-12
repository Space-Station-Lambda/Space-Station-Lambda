using System;
using Sandbox;
using Sandbox.UI;

namespace ssl.UI
{
    [UseTemplate]
    public class Hud : HudEntity<RootPanel>
    {
        public RoleSelector RoleSelector { get; set; }
        public Hud()
        {
            if (IsClient)
            { 
                RoleSelector = RootPanel.AddChild<RoleSelector>();
                RootPanel.AddChild<RoundInfos>();
            }
        }
    }
}