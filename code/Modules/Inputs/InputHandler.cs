using Sandbox;
using ssl.Player;

namespace ssl.Modules.Inputs
{
    public class InputHandler
    {
        private MainPlayer player;

        public InputHandler(MainPlayer player)
        {
            this.player = player;
        }

        public void CheckControls()
        {
            if (Input.Pressed(InputButton.Slot1)) player.Inventory.StartHolding(0);
            if (Input.Pressed(InputButton.Slot2)) player.Inventory.StartHolding(1);
            if (Input.Pressed(InputButton.Slot3)) player.Inventory.StartHolding(2);
            if (Input.Pressed(InputButton.Slot4)) player.Inventory.StartHolding(3);
            if (Input.Pressed(InputButton.Slot5)) player.Inventory.StartHolding(4);
            if (Input.Pressed(InputButton.Slot6)) player.Inventory.StartHolding(5);
            if (Input.Pressed(InputButton.Slot7)) player.Inventory.StartHolding(6);
            if (Input.Pressed(InputButton.Slot8)) player.Inventory.StartHolding(7);
            if (Input.Pressed(InputButton.Slot9)) player.Inventory.StartHolding(8);
            if (Input.Pressed(InputButton.Slot0)) player.Inventory.StartHolding(9);
            
            if (player.IsClient) CheckClientControls();
            if (player.IsServer) CheckServercontrols();
        }

        public void CheckClientControls()
        {
        }

        public void CheckServercontrols()
        {
            if (Input.Pressed(InputButton.Reload)) player.Respawn();
            if (Input.Pressed(InputButton.Drop)) player.Inventory.DropItem();
            if (Input.Pressed(InputButton.Use)) player.Selector.UseSelected();
        }
    }
}