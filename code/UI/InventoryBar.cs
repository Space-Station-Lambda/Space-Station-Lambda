using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
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
            for (int i = 0; i < 10; i++)
            {
                string name = (i + 1).ToString();
                if (i == 9) name = "0";
                icons[i] = new InventoryIcon(i, name, this);
            }
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
            
            if (input.Pressed(InputButton.Slot1)) SelectSlot(0);
            if (input.Pressed(InputButton.Slot2)) SelectSlot(1);
            if (input.Pressed(InputButton.Slot3)) SelectSlot(2);
            if (input.Pressed(InputButton.Slot4)) SelectSlot(3);
            if (input.Pressed(InputButton.Slot5)) SelectSlot(4);
            if (input.Pressed(InputButton.Slot6)) SelectSlot(5);
            if (input.Pressed(InputButton.Slot7)) SelectSlot(6);
            if (input.Pressed(InputButton.Slot8)) SelectSlot(7);
            if (input.Pressed(InputButton.Slot9)) SelectSlot(8);
            if (input.Pressed(InputButton.Slot0)) SelectSlot(9);

            if (input.MouseWheel != 0) SelectSlot(selected + input.MouseWheel);
        }

        private void RefreshAllModels()
        {
            foreach (InventoryIcon icon in icons)
            {
                icon.RefreshModel();
            }
        }
        
        public class InventoryIcon : Panel
        {
            private static readonly Angles angles = new(30, 180+45, 0);
            private static readonly Vector3 pos = new(10, 10, 10);
            
            private Scene scene;
            private SceneWorld sceneWorld;
            private SceneObject sceneObject;
            private Light sceneLight;
            
            public InventoryIcon(int slotNumber, string name, Panel parent)
            {
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
                
                MainPlayer player = (MainPlayer) Local.Client.Pawn;
                
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
}
