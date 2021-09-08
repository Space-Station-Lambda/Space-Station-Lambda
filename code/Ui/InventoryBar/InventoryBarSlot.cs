using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Ui.InventoryBar
{
    public class InventoryBarSlot : Panel
    {
        private const float fieldOfView = 50;
        private static readonly Angles angles = new(30, 180 + 45, 0);
        private static readonly Vector3 pos = new(10, 10, 10);
        private static readonly Vector3 focusSize = new(5, 5, 5);
        private Item currentItem;

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

            //Return if the old item is the same
            if (null != item && item.Equals(currentItem)) return;

            using (SceneWorld.SetCurrent(sceneWorld))
            {
                if (null == item)
                {
                    sceneObject?.Delete();
                    sceneObject = null;
                }
                else
                {
                    Model model = Model.Load(item.Data.Model);
                    Transform modelTransform = new Transform()
                        .WithPosition(-model.RenderBounds.Center)
                        .WithScale(focusSize.Length / (model.RenderBounds.Size.Length * 0.5f))
                        .WithRotation(Rotation.Identity);
                    sceneObject ??= new SceneObject(model, modelTransform);
                }

                sceneLight ??= Light.Point(Vector3.Up * 10.0f + Vector3.Forward * 100.0f - Vector3.Right * 100.0f,
                    2000, Color.White * 15000f);
            }

            scene ??= Add.ScenePanel(sceneWorld, pos, angles.ToRotation(), fieldOfView, "itemslot-model");
        }
    }
}