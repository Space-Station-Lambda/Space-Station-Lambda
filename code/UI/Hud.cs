﻿using System;
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
                RootPanel.AddChild<RoundInfos>();
                RootPanel.AddChild<InventoryBar>();
                RoleSelector = RootPanel.AddChild<RoleSelector>();
            }
        }

        public RoleSelector RoleSelector { get; set; }
    }
}