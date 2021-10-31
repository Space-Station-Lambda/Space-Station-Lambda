using Sandbox;
using ssl.Modules.Elements.Items.Carriables;

namespace ssl.Modules.Elements.Props.Triggers
{
    [Library("ssl_garbage_trigger")]
    [Hammer.AutoApplyMaterial( "materials/tools/toolstrigger.vmat" )]
    [Hammer.Solid]
    public partial class GarbageTrigger : BaseTrigger
    {
        public override void OnTouchStart(Entity toucher)
        {
            base.OnTouchStart(toucher);
            if (null == ((Item)toucher).Parent)
            {
                toucher.Delete();
            }
        }

        public override bool PassesTriggerFilters(Entity other)
        {
            return other.Tags.Has(Item.Tag);
        }
    }
}