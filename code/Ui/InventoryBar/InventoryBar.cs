using Sandbox;
using Sandbox.UI;

namespace ssl.Ui.InventoryBar
{
    public class InventoryBar : Panel
    {
        private InventoryBarSlot[] icons = new InventoryBarSlot[10];
        private int selected;

        public InventoryBar()
        {
            StyleSheet.Load("Ui/InventoryBar/InventoryBar.scss");
            for (int i = 0; i < 10; i++)
            {
                string name = (i + 1).ToString();
                if (i == 9) name = "0";
                icons[i] = new InventoryBarSlot(i, name, this);
            }

            //When spawn select the last slot probably empty
            //TODO FIXME Select slot 0 OnSpawn and refresh all models, first try to repair events
            SelectSlot(9);
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
            foreach (InventoryBarSlot icon in icons)
            {
                icon.RefreshModel();
            }
        }
    }
}