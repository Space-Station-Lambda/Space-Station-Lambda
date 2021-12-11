using Sandbox;
using Sandbox.Joints;

namespace ssl.Modules.Selection;

public class Dragger : Selector
{
	private const float HOLD_DISTANCE = 50F;
	private const float LINEAR_FREQUENCY = 10F;
	private const float LINEAR_DAMPING_RATIO = 1F;
	private const float ANGULAR_FREQUENCY = 10F;
	private const float ANGULAR_DAMPING_RATIO = 1F;
	private const float BREAK_LINEAR_FORCE = 2000F;

	private PhysicsBody holdBody;
	private WeldJoint holdJoint;

	public Dragger()
	{
		Activate();
	}

	public IDraggable Dragged { get; private set; }
	public PhysicsBody HeldBody { get; private set; }
	public Rotation HeldRot { get; private set; }

	/// <summary>
	///     Updates the Dragger system.
	///     Will start dragging item if there's not one already or will just update the position of the "hand".
	/// </summary>
	public void Drag()
	{
		if ( Dragged == null )
		{
			//Try to start dragging the entity currently targeted.
			if ( IsSelectedDraggable() )
			{
				TryDrag();
			}
		}
		else
		{
			// Updates the position of the "hand" of dragger. 
			GrabMove(Entity.EyePos, Entity.EyeRot.Forward, Entity.EyeRot);
		}
	}

	/// <summary>
	///     Start dragging an Entity if it is a draggable.
	///     Will stop dragging the previous one if there's any.
	/// </summary>
	/// <param name="grabPos">Destination of the dragged entity. (Relative to World)</param>
	/// <param name="grabRot">Ideal Rotation of the dragged entity. (Relative to World)</param>
	private void StartDrag( Vector3 grabPos, Rotation grabRot )
	{
		if ( !IsSelectedDraggable() )
		{
			return;
		}

		StopDrag();

		Dragged = (IDraggable)Selected;

		HeldBody = TraceResult.Body;
		HeldBody.Wake();
		HeldBody.EnableAutoSleeping = false;

		HeldRot = grabRot.Inverse * HeldBody.Rotation;

		holdBody.Position = grabPos;
		holdBody.Rotation = HeldBody.Rotation;

		holdJoint = PhysicsJoint.Weld
			.From(holdBody)
			.To(HeldBody, HeldBody.LocalMassCenter)
			.WithLinearSpring(LINEAR_FREQUENCY, LINEAR_DAMPING_RATIO, 0.0f)
			.WithAngularSpring(ANGULAR_FREQUENCY, ANGULAR_DAMPING_RATIO, 0.0f)
			.Breakable(HeldBody.Mass * BREAK_LINEAR_FORCE, 0)
			.Create();

		Dragged.OnDragStart(Entity);
	}

	/// <summary>
	///     Releases the Draggable currently being dragged, if there's one.
	/// </summary>
	public void StopDrag()
	{
		if ( holdJoint.IsValid )
		{
			holdJoint.Remove();
		}

		if ( Dragged != null )
		{
			HeldBody.EnableAutoSleeping = true;
			Dragged.OnDragStop(Entity);
		}

		HeldBody = null;
		Dragged = null;
		HeldRot = Rotation.Identity;
	}

	private bool IsSelectedDraggable()
	{
		if ( Selected is not IDraggable draggable )
		{
			return false;
		}

		if ( !TraceResult.Body.IsValid() )
		{
			return false;
		}

		if ( TraceResult.Body.PhysicsGroup == null )
		{
			return false;
		}

		return true;
	}

	private void TryDrag()
	{
		if ( ((IDraggable)Selected).IsDraggable(Entity) )
		{
			StartDrag(Entity.EyePos + Entity.EyeRot.Forward * HOLD_DISTANCE, Entity.EyeRot);
		}
	}

	private void GrabMove( Vector3 startPos, Vector3 dir, Rotation rot )
	{
		if ( !HeldBody.IsValid() )
		{
			return;
		}

		Dragged.OnDrag(Entity);

		holdBody.Position = startPos + dir * HOLD_DISTANCE;
		holdBody.Rotation = rot * HeldRot;
	}

	private void Activate()
	{
		if ( !holdBody.IsValid() )
		{
			holdBody = new PhysicsBody {BodyType = PhysicsBodyType.Keyframed};
		}
	}

	private void Deactivate()
	{
		holdBody?.Remove();
		holdBody = null;
	}
}
