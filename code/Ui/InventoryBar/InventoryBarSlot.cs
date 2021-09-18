using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Ui.InventoryBar
{
    public class InventoryBarSlot : Panel
    {
        private const float FieldOfView = 55;
        private static readonly Angles CamAngles = new(30, 180 + 45, 0);
        private static readonly Vector3 CamPos = new(10, 10, 8);
        private static readonly Vector3 FocusSize = new(9, 9, 9);

        private static readonly Vector3 RedLightPos = new(10,0,0);
        private static readonly Color RedLightColor = Color.Red * 0.1f;
        
        private static readonly Vector3 BlueLightPos = new(0,10,0);
        private static readonly Color BlueLightColor = Color.Blue * 0.1f;
        
        private static readonly Vector3 MainLightPos = CamPos;
        private static readonly Color MainLightColor = Color.White;
        
        private Item lastItem;
        private ScenePanel scene;
        private Light redLight;
        private Light blueLight;
        private Light mainLight;
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

            sceneWorld ??= new SceneWorld();

            Item item = player.Inventory.Get(SlotNumber);

            using (SceneWorld.SetCurrent(sceneWorld))
            {
                if (null == item)
                {
                    sceneObject?.Delete();
                    sceneObject = null;
                    lastItem = null;
                }
                else if (lastItem?.Data != item.Data)
                {
                    ApplyItem(item);
                }

                redLight ??= Light.Point(RedLightPos, 2000, RedLightColor);
                blueLight ??= Light.Point(BlueLightPos, 2000, BlueLightColor);
                mainLight ??= Light.Point(MainLightPos, 2000, MainLightColor);
            }

            scene ??= Add.ScenePanel(sceneWorld, CamPos, CamAngles.ToRotation(), FieldOfView, "itemslot-model");
        }

        private void ApplyItem(Item item)
        {
            Model model = Model.Load(item.Data.Model);
            if (!model.IsError)
            {
                float scaleFactor = FocusSize.Length / model.RenderBounds.Size.Length;
                Transform modelTransform = new Transform()
                    .WithPosition(-model.PhysicsBounds.Center * scaleFactor)
                    .WithScale(scaleFactor)
                    .WithRotation(Rotation.Identity);
                sceneObject ??= new SceneObject(model, modelTransform);
            }

            lastItem = item;
        }
    }
}