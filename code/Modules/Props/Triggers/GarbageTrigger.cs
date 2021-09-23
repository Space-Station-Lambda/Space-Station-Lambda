using Sandbox;

namespace ssl.Modules.Props.Triggers
{
    public class GarbageTrigger : BaseTrigger
    {
        public override void OnTouchStart(Entity toucher)
        {
            base.OnTouchStart(toucher);
            toucher.Delete();
        }
    }
}