using System.Collections.Generic;
using Hammer;
using Sandbox;

namespace ssl.Modules.Gravity;

/// <summary>
///     Defines an area with a special force field.
///     A force field is not a "shield" but more the association of a Force to a position in space.
/// </summary>
[AutoApplyMaterial()]
[Solid]
[VisGroup(VisGroup.Physics)]
public abstract class BaseForceField : ModelEntity
{
    public ISet<Entity> Entities { get; private set; }

    protected BaseForceField()
    {
        Entities = new HashSet<Entity>();
    }

    public override void Spawn()
    {
        base.Spawn();

        SetupPhysicsFromModel(PhysicsMotionType.Static);
        CollisionGroup = CollisionGroup.Trigger;
        EnableSolidCollisions = false;
        EnableTouch = true;

        Transmit = TransmitType.Never;
    }

    public override void StartTouch(Entity other)
    {
        base.StartTouch(other);

        if (other.IsWorld)
            return;

        AddEntity(other);
    }
    
    public override void Touch(Entity other)
    {
        base.StartTouch(other);

        if (other.IsWorld)
            return;

        AddEntity(other);
    }

    public override void EndTouch(Entity other)
    {
        base.EndTouch(other);

        if (other.IsWorld)
            return;
        
        if (Entities.Contains(other)) Entities.Remove(other);
    }

    protected virtual void AddEntity(Entity other)
    {
        Entities.Add(other);
    }

    /// <summary>
    ///     Gets the force from the field at the global position.
    /// </summary>
    /// <param name="pos">The position in global coordinates</param>
    /// <returns>The force vector at the position</returns>
    protected abstract Vector3 ForceFromGlobalPosition(Vector3 pos);

    [Event.Physics.PreStep]
    protected virtual void ApplyGravity()
    {
        foreach (Entity entity in Entities)
        {
            foreach (PhysicsBody body in entity.PhysicsGroup.Bodies)
            {
                body.ApplyForce(ForceFromGlobalPosition(body.Position) * body.Mass);
            }
        }
    }
}