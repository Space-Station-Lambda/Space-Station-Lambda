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

	    /// <summary>
	    /// The PhysicsBody used when the PhysicalProp will be dragged.
	    /// By default it's only the default PhysicsBody.
	    /// </summary>
	    public virtual PhysicsBody Body => PhysicsBody;

	    public override void Spawn()
        {
            base.Spawn();
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
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