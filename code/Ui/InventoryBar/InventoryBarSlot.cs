﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;

namespace ssl.Ui.InventoryBar
{
    public class InventoryBarSlot : Panel
    {
        private static readonly Angles angles = new(30, 180 + 45, 0);
        private static readonly Vector3 pos = new(10, 10, 10);

        private Scene scene;
        private Light sceneLight;
        private SceneObject sceneObject;
        private SceneWorld sceneWorld;

        public InventoryBarSlot(int slotNumber, string name, Panel parent)
        {
            StyleSheet.Load("Ui/InventoryBar/InventoryBarSlot.scss");
            SlotNumber = slotNumber;
            Parent = parent;
            Add.Label($"{name}");
            RefreshModel();
        }

        public int SlotNumber { get; private set; }

        public void RefreshModel()
        {
            if (Local.Client?.Pawn is not MainPlayer)
                return;

            MainPlayer player = (MainPlayer)Local.Client.Pawn;

            sceneWorld = new SceneWorld();

            using (SceneWorld.SetCurrent(sceneWorld))
            {
                if (player.Inventory.IsSlotEmpty(SlotNumber)) return;

                Model model = Model.Load(player.Inventory.Get(SlotNumber).Model);
                sceneObject = new SceneObject(model, Transform.Zero);
                sceneLight = Light.Point(Vector3.Up * 10.0f + Vector3.Forward * 100.0f - Vector3.Right * 100.0f,
                    2000, Color.White * 15000.0f);
            }

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