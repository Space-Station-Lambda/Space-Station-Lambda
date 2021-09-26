using Sandbox;

namespace ssl.Modules.Props.Types
{
    public class PhysicalProp : Prop
    {
        public override void Spawn()
        {
            base.Spawn();
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
            PhysicsEnabled = true;
            UsePhysicsCollision = true;
            MoveType = MoveType.Physics;
        }
    }
}