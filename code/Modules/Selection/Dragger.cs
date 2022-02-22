using Sandbox;

namespace ssl.Modules.Selection;

/// <summary>
///     Dragger allows the user to select draggable object to move when holding click.
/// </summary>
public class Dragger : Selector
{
    private const float HOLD_DISTANCE = 50F;
    private const float LINEAR_FREQUENCY = 10F;
    private const float LINEAR_DAMPING_RATIO = 1F;
    private const float ANGULAR_FREQUENCY = 10F;
    private const float ANGULAR_DAMPING_RATIO = 1F;
    private const float BREAK_LINEAR_FORCE = 2000F;

    private PhysicsBody holdBody;
    private FixedJoint holdJoint;

    public Dragger()
    {
        Activate();
    }

    /// <summary>
    ///     Entity dragged (item, prop, player etc ...)
    /// </summary>
    public IDraggable Dragged { get; private set; }

    /// <summary>
    ///     PhysicsBody of the Draggable currently moved by the Dragger
    /// </summary>
    public PhysicsBody HeldBody { get; private set; }

    /// <summary>
    ///     Rotation of the current drag.
    /// </summary>
    public Rotation HeldRot { get; private set; }

    /// <summary>
    ///     Updates the Dragger system.
    ///     Will start dragging item if there's not one already or will just update the position of the "hand".
    /// </summary>
    public void Drag()
    {
        if (Dragged == null)
        {
            //Try to start dragging the entity currently targeted.
            if (IsSelectedDraggable()) TryDrag();
        }
        else
        {
            // Updates the position of the "hand" of dragger. 
            GrabMove(Entity.EyePosition, Entity.EyeRotation.Forward, Entity.EyeRotation);
        }
    }

    /// <summary>
    ///     Start dragging an Entity if it is a draggable.
    ///     Will stop dragging the previous one if there's any.
    /// </summary>
    /// <param name="grabPos">Destination of the dragged entity. (Relative to World)</param>
    /// <param name="grabRot">Ideal Rotation of the dragged entity. (Relative to World)</param>
    private void StartDrag(Vector3 grabPos, Rotation grabRot)
    {
        StopDrag();

        Dragged = (IDraggable) Selected;

        HeldBody = TraceResult.Body;
        HeldBody.AutoSleep = false;
        
        HeldRot = grabRot.Inverse * HeldBody.Rotation;

        holdBody.Position = grabPos;
        holdBody.Rotation = HeldBody.Rotation;

        holdJoint = PhysicsJoint.CreateFixed(holdBody.LocalPoint(Vector3.Zero), HeldBody.MassCenterPoint());
        holdJoint.SpringLinear = new PhysicsSpring(LINEAR_FREQUENCY, LINEAR_DAMPING_RATIO);
        holdJoint.SpringAngular = new PhysicsSpring(ANGULAR_FREQUENCY, ANGULAR_DAMPING_RATIO);
        holdJoint.Strength = HeldBody.Mass * BREAK_LINEAR_FORCE;

        Dragged.OnDragStart(Entity);
    }

    /// <summary>
    ///     Releases the Draggable currently being dragged, if there's one.
    /// </summary>
    public void StopDrag()
    {
        if (holdJoint.IsValid()) holdJoint.Remove();

        if (Dragged != null)
        {
            HeldBody.AutoSleep = true;
            Dragged.OnDragStop(Entity);
        }

        HeldBody = null;
        Dragged = null;
        HeldRot = Rotation.Identity;
    }

    /// <summary>
    ///     Detect if the selected object is draggable.
    /// </summary>
    /// <returns></returns>
    private bool IsSelectedDraggable()
    {
        // Check 
        if (Selected is not IDraggable) return false;

        if (!TraceResult.Body.IsValid()) return false;

        return TraceResult.Body.PhysicsGroup != null;
    }

    /// <summary>
    ///     Try to drag an entity.
    /// </summary>
    private void TryDrag()
    {
        if (((IDraggable) Selected).IsDraggable(Entity))
            StartDrag(Entity.EyePosition + Entity.EyeRotation.Forward * HOLD_DISTANCE, Entity.EyeRotation);
    }

    private void GrabMove(Vector3 startPos, Vector3 dir, Rotation rot)
    {
        if (!HeldBody.IsValid()) return;

        Dragged.OnDrag(Entity);

        holdBody.Position = startPos + dir * HOLD_DISTANCE;
        holdBody.Rotation = rot * HeldRot;
    }

    private void Activate()
    {
        if (!holdBody.IsValid()) holdBody = new PhysicsBody(Map.Physics) { BodyType = PhysicsBodyType.Keyframed };
    }

    private void Deactivate()
    {
        holdBody?.Remove();
        holdBody = null;
    }
}