using ssl.Modules.Items.Data;
using ssl.Modules.Props.Instances;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Instances;

public class ItemCleaner : Item
{
	/// <summary>
	///     The dirtyness is the level of sanity of the cleaner. 0 Is perfect and the max is to be defined.
	/// </summary>
	public int Dirtyness { get; private set; }

	public int CleaningValue { get; set; }

	public override void OnUsePrimary( SslPlayer sslPlayer, ISelectable target )
	{
		switch ( target )
		{
			case PropBucket bucket:
				Wash(bucket);
				break;
			default:
				target.OnInteract(sslPlayer, CleaningValue);
				break;
		}
	}

	/// <summary>
	///     wash the cleaner in a bucket for set the dirty level to 0.
	/// </summary>
	/// <param name="propBucket"></param>
	private void Wash( PropBucket propBucket )
	{
		propBucket.WasteWater(Dirtyness);
		Dirtyness = 0;
	}
}
