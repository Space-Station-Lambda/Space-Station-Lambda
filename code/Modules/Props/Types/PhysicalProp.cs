namespace ssl.Modules.Props.Types
{
    public class PhysicalProp : Prop
    {
        public override void Spawn()
        {
            base.Spawn();
            PhysicsEnabled = true;
        }
    }
}