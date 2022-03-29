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
    ///     Gets the force from the field for a given entity
    /// </summary>
    /// <param name="ent">The entity that will receive the force</param>
    /// <returns>The force vector for the entity</returns>
    protected abstract Vector3 ForceFromEntity(Entity ent);

    /// <summary>
    ///     Default implementation for applying the force to all the entities.
    ///     This can be overriden to change the behaviour if the force field is special.
    /// </summary>
    [Event.Physics.PreStep]
    protected virtual void ApplyForce()
    {
        foreach (Entity entity in Entities)
        {
            foreach (PhysicsBody body in entity.PhysicsGroup.Bodies)
            {
                body.ApplyForce(ForceFromEntity(entity) * body.Mass);
            }
        }
    }
}