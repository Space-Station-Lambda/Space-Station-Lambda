using Sandbox;
using ssl.Modules.Elements.Items.Carriables;
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

            if (Input.Pressed(InputButton.Attack1)) player.Inventory.UsePrimary();
            if (Input.Pressed(InputButton.Attack2)) player.Inventory.UseSecondary();
            
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
                player.Dragger.UseSelected();
            }

            //TODO: Drag Inputs (long hold to drag, etc.)
            if (Input.Down(InputButton.Attack1))
            {
                player.Dragger.Drag();
            } 
            else if (Input.Released(InputButton.Attack1))
            {
                player.Dragger.StopDrag();
            }

            if (Input.Pressed(InputButton.Flashlight))
            {
                player.RagdollHandler.StartRagdoll();
                player.RagdollHandler.TimeExitRagdoll = Time.Now + 1;
            }
        }
    }
}