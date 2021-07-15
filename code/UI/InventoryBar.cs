using System.Collections.Generic;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ssl.UI
{
    public class InventoryBar : Panel
    {

        private InventoryIcon[] icons = new InventoryIcon[10];
        
        public InventoryBar()
        {
            StyleSheet.Load( "ui/InventoryBar.scss" );
            for (int i = 1; i < 10; i++)
            {
                icons[i] = new InventoryIcon(i, this);
            }
            icons[0] = new InventoryIcon(0, this);
        }

        public class InventoryIcon : Panel
        {
            public int SlotNumber { get; private set; }
            public InventoryIcon(int slotNumber, Panel parent)
            {
                if (slotNumber == 4)
                {
                    SetClass("selected", true);
                }
                SlotNumber = slotNumber;
                Parent = parent;
                Add.Label($"{SlotNumber}");
            }
        }
        
    }
}