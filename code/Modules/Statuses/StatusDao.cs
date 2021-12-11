using ssl.Commons;

namespace ssl.Modules.Statuses;

public class StatusDao : LocalDao<StatusData>
{
	private static StatusDao instance;

	private StatusDao()
	{
	}
	
	public static StatusDao Instance => instance ??= new StatusDao();

	/// <summary>
	///     Load all data from disk files.
	/// </summary>
	protected override void LoadAll()
	{
		Log.Info("Load items..");

		// Statuses
		Log.Info($"{All.Count} items charged !");
	}
}
