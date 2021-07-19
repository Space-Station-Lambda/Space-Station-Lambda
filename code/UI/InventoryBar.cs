using System;
using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Items;
using ssl.Items.Data;
using ssl.Player;

namespace ssl.UI
{
    public class InventoryBar : Panel
    {
        private InventoryIcon[] icons = new InventoryIcon[10];
        private int selected;

        public InventoryBar()
        {
            StyleSheet.Load("ui/InventoryBar.scss");
            for (int i = 1; i < 10; i++)
            {
                icons[i] = new InventoryIcon(i, this);
            }

            icons[0] = new InventoryIcon(0, this);
            SelectSlot(0);
        }

        public void SelectSlot(int slot)
        {
            icons[selected].SetClass("selected", false);
            selected = slot;
            if (selected < 0) selected = 9;
            if (selected > 9) selected = 0;
            icons[selected].SetClass("selected", true);
            icons[selected].RefreshModel();
            ConsoleSystem.Run("set_inventory_holding", selected);
        }

        [Event("buildinput")]
        public void ProcessClientInput(InputBuilder input)
        {
            if (Local.Pawn is not MainPlayer player)
                return;

            if (input.Pressed(InputButton.Slot1)) SelectSlot(1);
            if (input.Pressed(InputButton.Slot2)) SelectSlot(2);
            if (input.Pressed(InputButton.Slot3)) SelectSlot(3);
            if (input.Pressed(InputButton.Slot4)) SelectSlot(4);
            if (input.Pressed(InputButton.Slot5)) SelectSlot(5);
            if (input.Pressed(InputButton.Slot6)) SelectSlot(6);
            if (input.Pressed(InputButton.Slot7)) SelectSlot(7);
            if (input.Pressed(InputButton.Slot8)) SelectSlot(8);
            if (input.Pressed(InputButton.Slot9)) SelectSlot(9);
            if (input.Pressed(InputButton.Slot0)) SelectSlot(0);

            if (input.MouseWheel != 0) SelectSlot(selected + input.MouseWheel);
        }
        
        public class InventoryIcon : Panel
        {
            private Scene scene;
            private SceneWorld sceneWorld;
            private SceneObject sceneObject;
            private Light sceneLight;
            
            public InventoryIcon(int slotNumber, Panel parent)
            {
                SlotNumber = slotNumber;
                Parent = parent;
                Add.Label($"{SlotNumber}");
                RefreshModel();
            }

            public int SlotNumber { get; private set; }

            public void RefreshModel()
            {
                if (Local.Client?.Pawn is not MainPlayer)
                    return;
                
                MainPlayer player = (MainPlayer) Local.Client.Pawn;
                
                sceneWorld = new SceneWorld();
                Log.Info(player.Inventory.SlotsFull);

                using (SceneWorld.SetCurrent(sceneWorld))
                {
                    if (!player.Inventory.IsSlotEmpty(SlotNumber))
                    {
                        Model model = Model.Load("models/knife/knife.vmdl");
                        sceneObject = new SceneObject(model, Transform.Zero);
                        sceneLight = Light.Point(Vector3.Up * 10.0f + Vector3.Forward * 100.0f - Vector3.Right * 100.0f,
                            2000, Color.White * 15000.0f);
                    }
                }
            
                Angles angles = new(30, 180+45, 0);
                Vector3 pos = new(10, 10, 10);
                if (scene != null)
                {
                    scene.World = sceneWorld;
                }
                else
                {
                    scene = Add.Scene(sceneWorld, pos, angles, 50, "itemslot-model");
                }
            }
        }
    }
}