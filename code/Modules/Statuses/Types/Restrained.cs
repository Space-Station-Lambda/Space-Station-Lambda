using ssl.Modules.Items.Instances;

namespace ssl.Modules.Statuses.Types;

/// <summary>
///     The restrained status makes the player can't use anything and can only walk
/// </summary>
public class Restrained : Status
{
	public Restrained( ItemRestrain restrain )
	{
		Restrain = restrain;
	}

	/// <summary>
	///     Restrain item used
	/// </summary>
	public ItemRestrain Restrain { get; }
}
