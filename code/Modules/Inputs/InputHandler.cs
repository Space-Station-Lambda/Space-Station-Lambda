using Sandbox;
using ssl.Modules.Items.Instances;
using ssl.Player;

namespace ssl.Modules.Inputs;

public partial class InputHandler : EntityComponent<SslPlayer>
{
	private const int DefaultStrength = 1;

	public InputHandler()
	{
		TimeSinceLastUse = Cooldown;
	}

	//TODO : Add a way to change the cooldown by the skill of the player
	[Net] private float Cooldown { get; } = 0.5f;

	[Net] private TimeSince TimeSinceLastUse { get; set; }

	public void CheckControls()
	{
		// If the player don't drag anything
		if ( null == Entity.Dragger.Dragged )
		{
			if ( Input.Pressed(InputButton.Slot1) )
			{
				Entity.Inventory.ProcessHolding(0);
			}

			if ( Input.Pressed(InputButton.Slot2) )
			{
				Entity.Inventory.ProcessHolding(1);
			}

			if ( Input.Pressed(InputButton.Slot3) )
			{
				Entity.Inventory.ProcessHolding(2);
			}

			if ( Input.Pressed(InputButton.Slot4) )
			{
				Entity.Inventory.ProcessHolding(3);
			}

			if ( Input.Pressed(InputButton.Slot5) )
			{
				Entity.Inventory.ProcessHolding(4);
			}

			if ( Input.Pressed(InputButton.Slot6) )
			{
				Entity.Inventory.ProcessHolding(5);
			}

			if ( Input.Pressed(InputButton.Slot7) )
			{
				Entity.Inventory.ProcessHolding(6);
			}

			if ( Input.Pressed(InputButton.Slot8) )
			{
				Entity.Inventory.ProcessHolding(7);
			}

			if ( Input.Pressed(InputButton.Slot9) )
			{
				Entity.Inventory.ProcessHolding(8);
			}

			if ( Input.Pressed(InputButton.Slot0) )
			{
				Entity.Inventory.ProcessHolding(9);
			}
		}

		if ( Entity.IsClient )
		{
			CheckClientControls();
		}

		if ( Entity.IsServer )
		{
			CheckServercontrols();
		}
	}

	private void CheckClientControls()
	{
	}

	private void CheckServercontrols()
	{
		// Reload 

		if ( Input.Pressed(InputButton.Reload) )
		{
			Entity.Respawn();
		}

		// Drop

		if ( Input.Pressed(InputButton.Drop) )
		{
			Item dropped = Entity.Inventory.DropItem();
			dropped.Velocity += Entity.Velocity;
		}

		// Usage system
		if ( TimeSinceLastUse >= Cooldown )
		{
			// Default usage with the use button
			if ( Input.Down(InputButton.Use) )
			{
				Entity.Dragger.UseSelected();
			}

			// Primary Action
			if ( Input.Down(InputButton.Attack1) )
			{
				Entity.Inventory.UsePrimary();
			}

			// Secondary Action and drag
			if ( Input.Down(InputButton.Attack2) )
			{
				// Drag only if empty hand
				if ( null == Entity.Inventory.HoldingItem )
				{
					Entity.Dragger.Drag();
				}
				else
				{
					Entity.Inventory.UseSecondary();
				}
			}

			TimeSinceLastUse = 0;
		}

		;
		if ( Input.Released(InputButton.Attack2) )
		{
			Entity.Dragger.StopDrag();
		}

		// Flashlight

		if ( Input.Pressed(InputButton.Flashlight) )
		{
			Entity.RagdollHandler.StartRagdoll();
		}
	}
}
