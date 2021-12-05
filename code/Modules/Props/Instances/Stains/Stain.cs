using Sandbox;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Instances.Stains;

public class Stain : Prop, IWorkable
{
	public Stain()
	{
		CollisionGroup = CollisionGroup.Trigger;
	}

	public int RemainingWork { get; set; } = 10;

	public void OnWorkDone( SslPlayer player )
	{
		Delete();
	}
}
