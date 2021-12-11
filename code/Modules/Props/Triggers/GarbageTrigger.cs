using Hammer;
using Sandbox;
using ssl.Modules.Items.Instances;

namespace ssl.Modules.Props.Triggers;

[Library("ssl_garbage_trigger")]
[AutoApplyMaterial]
[Solid]
public class GarbageTrigger : BaseTrigger
{
	public override void OnTouchStart( Entity toucher )
	{
		base.OnTouchStart(toucher);
		if ( null == ((Item)toucher).Parent )
		{
			toucher.Delete();
		}
	}

	public override bool PassesTriggerFilters( Entity other )
	{
		return other.Tags.Has(Item.TAG);
	}
}
