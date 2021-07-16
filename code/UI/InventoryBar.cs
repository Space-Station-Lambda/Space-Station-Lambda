using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;

namespace ssl.UI
{
    public class InventoryBar : Panel
    {
        private int selected;
        private InventoryIcon[] icons = new InventoryIcon[10];
        
        public InventoryBar()
        {
            StyleSheet.Load( "ui/InventoryBar.scss" );
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
            
            MainPlayer player = (MainPlayer)Local.Pawn;
            player?.SetHolding(slot);
        }

        [Event( "buildinput" )]
        public void ProcessClientInput( InputBuilder input )
        {
            if ( Local.Pawn is not MainPlayer player )
                return;
            

            if ( input.Pressed( InputButton.Slot1 ) ) SelectSlot( 1 );
            if ( input.Pressed( InputButton.Slot2 ) ) SelectSlot( 2 );
            if ( input.Pressed( InputButton.Slot3 ) ) SelectSlot( 3 );
            if ( input.Pressed( InputButton.Slot4 ) ) SelectSlot( 4 );
            if ( input.Pressed( InputButton.Slot5 ) ) SelectSlot( 5 );
            if ( input.Pressed( InputButton.Slot6 ) ) SelectSlot( 6 );
            if ( input.Pressed( InputButton.Slot7 ) ) SelectSlot( 7 );
            if ( input.Pressed( InputButton.Slot8 ) ) SelectSlot( 8 );
            if ( input.Pressed( InputButton.Slot9 ) ) SelectSlot( 9 );
            if ( input.Pressed( InputButton.Slot0 ) ) SelectSlot( 0 );

            if ( input.MouseWheel != 0 ) SelectSlot(selected + input.MouseWheel);
        }

        public class InventoryIcon : Panel
        {
            public int SlotNumber { get; private set; }
            public InventoryIcon(int slotNumber, Panel parent)
            {
                SlotNumber = slotNumber;
                Parent = parent;
                Add.Label($"{SlotNumber}");
            }
        }
        
    }
}