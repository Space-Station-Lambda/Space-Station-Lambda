using Sandbox;
using ssl.Modules.Elements.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Inputs
{
    public partial class InputHandler : EntityComponent<SslPlayer>
    {
        
        public InputHandler()
        {
            TimeSinceLastUse = Cooldown;
        }
        
        //TODO : Add a way to change the cooldown by the skill of the player
        [Net] private float Cooldown { get; set; } = 0.5f;

        [Net] private TimeSince TimeSinceLastUse { get; set; }

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
            // Reload 
            
            if (Input.Pressed(InputButton.Reload))
            {
                Entity.Respawn();
            }
            
            // Drop
            
            if (Input.Pressed(InputButton.Drop))
            {
                Item dropped = Entity.Inventory.DropItem();
                dropped.Velocity += Entity.Velocity;
            }
            
            // Use
            
            // 

            if (Input.Down(InputButton.Use))
            {
                if (TimeSinceLastUse >= Cooldown)
                {
                    Entity.Dragger.UseSelected();
                    TimeSinceLastUse = 0;
                }
            }
            
            // Attack
            
            if (Input.Down(InputButton.Attack1))
            {
                Entity.Dragger.Drag();
            }

            else if (Input.Released(InputButton.Attack1))
            {
                Entity.Dragger.StopDrag();
            }

            // Flashlight
            
            if (Input.Pressed(InputButton.Flashlight))
            {
                Entity.RagdollHandler.StartRagdoll();
            }
        }
    }
}