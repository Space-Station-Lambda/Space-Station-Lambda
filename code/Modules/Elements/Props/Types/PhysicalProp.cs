using Sandbox;
using ssl.Modules.Elements.Props.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Elements.Props.Types
{
    public class PhysicalProp : Prop, IDraggable
    {
	    public PhysicalProp()
	    {
	    }

	    public PhysicalProp(PropData data) : base(data)
	    {
	    }

	    public override void Spawn()
        {
            base.Spawn();
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
            PhysicsEnabled = true;
            UsePhysicsCollision = true;
            MoveType = MoveType.Physics;
        }

        public void OnDragStart(SslPlayer sslPlayer)
        {
        }

        public void OnDragStop(SslPlayer sslPlayer)
        {
        }

        public void OnDrag(SslPlayer sslPlayer)
        {
        }

        public bool IsDraggable(SslPlayer sslPlayer)
        {
            return true;
        }
    }
}