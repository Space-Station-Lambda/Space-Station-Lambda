﻿using Sandbox;
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
                RootPanel.AddChild<RoleSelector>();
            }
        }
    }
}