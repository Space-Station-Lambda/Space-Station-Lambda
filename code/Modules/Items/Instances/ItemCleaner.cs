using Sandbox;
using ssl.Constants;
using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;
using Prop = ssl.Modules.Props.Instances.Prop;

namespace ssl.Modules.Items.Instances;

public class ItemCleaner : Item
{
	/// <summary>
	///     The dirtyness is the level of sanity of the cleaner. 0 Is perfect and the max is to be defined.
	/// </summary>
	public int Dirtyness { get; private set; }

    public int CleaningValue { get; set; }

	public override void OnDownUsePrimary(SslPlayer sslPlayer, ISelectable target)
	{
		if (target is Prop { Id: Identifiers.Props.BUCKET_ID })
		{
			Wash();
		}
		else
		{
			// TODO: Missing TraceResult
			target.OnInteract(sslPlayer, CleaningValue, new TraceResult());
		}
	}

	/// <summary>
	///     wash the cleaner in a bucket for set the dirty level to 0.
	/// </summary>
	private void Wash()
	{
		Dirtyness = 0;
	}
}