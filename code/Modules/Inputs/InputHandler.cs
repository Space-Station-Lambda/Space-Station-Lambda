using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Props.Types;
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
            if (Input.Pressed(InputButton.Slot1)) player.Inventory.ProcessHolding(0);
            if (Input.Pressed(InputButton.Slot2)) player.Inventory.ProcessHolding(1);
            if (Input.Pressed(InputButton.Slot3)) player.Inventory.ProcessHolding(2);
            if (Input.Pressed(InputButton.Slot4)) player.Inventory.ProcessHolding(3);
            if (Input.Pressed(InputButton.Slot5)) player.Inventory.ProcessHolding(4);
            if (Input.Pressed(InputButton.Slot6)) player.Inventory.ProcessHolding(5);
            if (Input.Pressed(InputButton.Slot7)) player.Inventory.ProcessHolding(6);
            if (Input.Pressed(InputButton.Slot8)) player.Inventory.ProcessHolding(7);
            if (Input.Pressed(InputButton.Slot9)) player.Inventory.ProcessHolding(8);
            if (Input.Pressed(InputButton.Slot0)) player.Inventory.ProcessHolding(9);

            if (player.IsClient) CheckClientControls();
            if (player.IsServer) CheckServercontrols();
        }

        public void CheckClientControls()
        {
        }

        public void CheckServercontrols()
        {
            if (Input.Pressed(InputButton.Reload)) player.Respawn();
            if (Input.Pressed(InputButton.Drop))
            {
                Item dropped = player.Inventory.DropItem();
                dropped.Velocity += player.Velocity;
            }

            if (Input.Pressed(InputButton.Use))
            {
                player.Selector.UseSelected();
            }
        }
    }
}