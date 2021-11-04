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

        public void OnDragStart(Player.Player player)
        {
        }

        public void OnDragStop(Player.Player player)
        {
        }

        public void OnDrag(Player.Player player)
        {
        }

        public bool IsDraggable(Player.Player player)
        {
            return true;
        }
    }
}