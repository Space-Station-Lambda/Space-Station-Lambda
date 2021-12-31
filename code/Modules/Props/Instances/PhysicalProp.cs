using Sandbox;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Instances;

public class PhysicalProp : Prop, IDraggable
{
    public void OnDragStart(SslPlayer sslPlayer) { }

    public void OnDragStop(SslPlayer sslPlayer) { }

    public void OnDrag(SslPlayer sslPlayer) { }

    public bool IsDraggable(SslPlayer sslPlayer)
    {
        return true;
    }

    public override void Spawn()
    {
        base.Spawn();
        SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
        PhysicsEnabled = true;
        UsePhysicsCollision = true;
        MoveType = MoveType.Physics;
    }
}