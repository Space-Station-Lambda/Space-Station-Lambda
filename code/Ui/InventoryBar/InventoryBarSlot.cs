using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Ui.InventoryBar
{
    public class InventoryBarSlot : Panel
    {
        private const float FieldOfView = 50;
        private const float ScaleMultiplier = 2F;
        private static readonly Angles angles = new(30, 180 + 45, 0);
        private static readonly Vector3 pos = new(10, 10, 10);
        private static readonly Vector3 focusSize = new(5, 5, 5);

        private Item lastItem;
        private ScenePanel scene;
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

                sceneLight ??= Light.Point(Vector3.Up * 10.0f + Vector3.Forward * 100.0f - Vector3.Right * 100.0f,
                    2000, Color.White);
            }

            scene ??= Add.ScenePanel(sceneWorld, pos, angles.ToRotation(), FieldOfView, "itemslot-model");
        }

        private void ApplyItem(Item item)
        {
            Model model = Model.Load(item.Data.Model);
            if (!model.IsError)
            {
                float scaleFactor = (focusSize.Length / model.RenderBounds.Size.Length) * ScaleMultiplier;
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