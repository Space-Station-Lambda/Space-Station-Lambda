using Sandbox;
using ssl.Modules.Elements.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Inputs
{
    public class InputHandler : EntityComponent<SslPlayer>
    {
        
        public void CheckControls()
        {
            if (Input.Pressed(InputButton.Slot1)) Entity.Inventory.ProcessHolding(0);
            if (Input.Pressed(InputButton.Slot2)) Entity.Inventory.ProcessHolding(1);
            if (Input.Pressed(InputButton.Slot3)) Entity.Inventory.ProcessHolding(2);
            if (Input.Pressed(InputButton.Slot4)) Entity.Inventory.ProcessHolding(3);
            if (Input.Pressed(InputButton.Slot5)) Entity.Inventory.ProcessHolding(4);
            if (Input.Pressed(InputButton.Slot6)) Entity.Inventory.ProcessHolding(5);
            if (Input.Pressed(InputButton.Slot7)) Entity.Inventory.ProcessHolding(6);
            if (Input.Pressed(InputButton.Slot8)) Entity.Inventory.ProcessHolding(7);
            if (Input.Pressed(InputButton.Slot9)) Entity.Inventory.ProcessHolding(8);
            if (Input.Pressed(InputButton.Slot0)) Entity.Inventory.ProcessHolding(9);

            if (Input.Pressed(InputButton.Attack1)) Entity.Inventory.UsePrimary();
            if (Input.Pressed(InputButton.Attack2)) Entity.Inventory.UseSecondary();
            
            if (Entity.IsClient) CheckClientControls();
            if (Entity.IsServer) CheckServercontrols();
        }

        public void CheckClientControls()
        {
        }

        public void CheckServercontrols()
        {
            if (Input.Pressed(InputButton.Reload)) Entity.Respawn();
            if (Input.Pressed(InputButton.Drop))
            {
                Item dropped = Entity.Inventory.DropItem();
                dropped.Velocity += Entity.Velocity;
            }

            if (Input.Pressed(InputButton.Use))
            {
                Entity.Dragger.UseSelected();
            }

            //TODO: Drag Inputs (long hold to drag, etc.)
            if (Input.Down(InputButton.Attack1))
            {
                Entity.Dragger.Drag();
            } 
            else if (Input.Released(InputButton.Attack1))
            {
                Entity.Dragger.StopDrag();
            }

            if (Input.Pressed(InputButton.Flashlight))
            {
                Entity.RagdollHandler.StartRagdoll();
            }
        }
    }
}