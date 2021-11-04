using Sandbox;
using ssl.Modules.Elements.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Inputs
{
    public class InputHandler
    {
        private Player.SslPlayer sslPlayer;

        public InputHandler(Player.SslPlayer sslPlayer)
        {
            this.sslPlayer = sslPlayer;
        }

        public void CheckControls()
        {
            if (Input.Pressed(InputButton.Slot1)) sslPlayer.Inventory.ProcessHolding(0);
            if (Input.Pressed(InputButton.Slot2)) sslPlayer.Inventory.ProcessHolding(1);
            if (Input.Pressed(InputButton.Slot3)) sslPlayer.Inventory.ProcessHolding(2);
            if (Input.Pressed(InputButton.Slot4)) sslPlayer.Inventory.ProcessHolding(3);
            if (Input.Pressed(InputButton.Slot5)) sslPlayer.Inventory.ProcessHolding(4);
            if (Input.Pressed(InputButton.Slot6)) sslPlayer.Inventory.ProcessHolding(5);
            if (Input.Pressed(InputButton.Slot7)) sslPlayer.Inventory.ProcessHolding(6);
            if (Input.Pressed(InputButton.Slot8)) sslPlayer.Inventory.ProcessHolding(7);
            if (Input.Pressed(InputButton.Slot9)) sslPlayer.Inventory.ProcessHolding(8);
            if (Input.Pressed(InputButton.Slot0)) sslPlayer.Inventory.ProcessHolding(9);

            if (Input.Pressed(InputButton.Attack1)) sslPlayer.Inventory.UsePrimary();
            if (Input.Pressed(InputButton.Attack2)) sslPlayer.Inventory.UseSecondary();
            
            if (sslPlayer.IsClient) CheckClientControls();
            if (sslPlayer.IsServer) CheckServercontrols();
        }

        public void CheckClientControls()
        {
        }

        public void CheckServercontrols()
        {
            if (Input.Pressed(InputButton.Reload)) sslPlayer.Respawn();
            if (Input.Pressed(InputButton.Drop))
            {
                Item dropped = sslPlayer.Inventory.DropItem();
                dropped.Velocity += sslPlayer.Velocity;
            }

            if (Input.Pressed(InputButton.Use))
            {
                sslPlayer.Dragger.UseSelected();
            }

            //TODO: Drag Inputs (long hold to drag, etc.)
            if (Input.Down(InputButton.Attack1))
            {
                sslPlayer.Dragger.Drag();
            } 
            else if (Input.Released(InputButton.Attack1))
            {
                sslPlayer.Dragger.StopDrag();
            }

            if (Input.Pressed(InputButton.Flashlight))
            {
                sslPlayer.RagdollHandler.StartRagdoll();
            }
        }
    }
}