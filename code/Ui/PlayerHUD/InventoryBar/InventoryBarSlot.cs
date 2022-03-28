using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Items.Instances;
using ssl.Player;

namespace ssl.Ui.InventoryBar;

public class InventoryBarSlot : Panel
{
    private const float FIELD_OF_VIEW = 55;

    private const float LIGHT_RADIUS = 2000F;
    private static readonly Angles CamAngles = new(30, 180 + 45, 0);
    private static readonly Vector3 CamPos = new(10, 10, 8);
    private static readonly Vector3 FocusSize = new(9, 9, 9);
    private static readonly Vector3 RedLightPos = new(10, 0, 0);
    private static readonly Color RedLightColor = Color.Red * 0.1f;

    private static readonly Vector3 BlueLightPos = new(0, 10, 0);
    private static readonly Color BlueLightColor = Color.Blue * 0.1f;

    private static readonly Vector3 MainLightPos = CamPos;
    private static readonly Color MainLightColor = Color.White;
    private SceneLight blueLight;

    private Item lastItem;
    private SceneLight mainLight;
    private SceneLight redLight;
    private ScenePanel scene;
    private SceneObject sceneObject;
    private SceneWorld sceneWorld;

    public InventoryBarSlot(int slotNumber, string name, Panel parent)
    {
        StyleSheet.Load("Ui/PlayerHUD/InventoryBar/InventoryBarSlot.scss");
        SlotNumber = slotNumber;
        Parent = parent;
        Add.Label($"{name}");
        RefreshModel();
    }

    public int SlotNumber { get; }

    public void RefreshModel()
    {
        if (Local.Client?.Pawn is not SslPlayer) return;

        SslPlayer sslPlayer = (SslPlayer) Local.Client.Pawn;

        sceneWorld ??= new SceneWorld();

        Item item = sslPlayer.Inventory.Get(SlotNumber);

        
        if (null == item)
        {
            sceneObject?.Delete();
            sceneObject = null;
            lastItem = null;
        }
        else if (null != lastItem)
        {
            if (lastItem.Id != item.Id) ApplyItem(item);
        }
        else
        {
            ApplyItem(item);
        }

        redLight ??= new SceneLight(sceneWorld, RedLightPos, LIGHT_RADIUS, RedLightColor);
        blueLight ??= new SceneLight(sceneWorld, BlueLightPos, LIGHT_RADIUS, BlueLightColor);
        mainLight ??= new SceneLight(sceneWorld, MainLightPos, LIGHT_RADIUS, MainLightColor);
        

        scene ??= Add.ScenePanel(sceneWorld, CamPos, CamAngles.ToRotation(), FIELD_OF_VIEW, "itemslot-model");
    }

    private void ApplyItem(Item item)
    {
        Model model = Model.Load(item.Model.Name);
        if (!model.IsError)
        {
            sceneObject?.Delete();
            float scaleFactor = FocusSize.Length / model.RenderBounds.Size.Length;
            Transform modelTransform = new Transform()
                .WithPosition(-model.PhysicsBounds.Center * scaleFactor)
                .WithScale(scaleFactor)
                .WithRotation(Rotation.Identity);
            sceneObject = new SceneObject(sceneWorld, model, modelTransform);
        }

        lastItem = item;
    }

    public override void OnDeleted()
    {
        base.OnDeleted();
        sceneWorld.Delete();
    }
}