using Sandbox;
using ssl.Modules.Props.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Types
{
    public class PhysicalProp : Prop, IDraggable
    {
	    public PhysicalProp()
	    {
	    }

	    public PhysicalProp(PropData data) : base(data)
	    {
	    }

	    public PhysicsBody Body => PhysicsBody;

	    public override void Spawn()
        {
            base.Spawn();
            PhysicsEnabled = true;
            UsePhysicsCollision = true;
            MoveType = MoveType.Physics;
        }

        public void OnDragStart(MainPlayer player)
        {
        }

        public void OnDragStop(MainPlayer player)
        {
        }

        public void OnDrag(MainPlayer player)
        {
        }

        public bool IsDraggable(MainPlayer player)
        {
            return true;
        }
    }
}