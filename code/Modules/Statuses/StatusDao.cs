using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Statuses.Data;

namespace ssl.Modules.Statuses;

public class StatusDao : LocalDao<StatusData>
{
    private static StatusDao instance;

    private StatusDao() { }

    public static StatusDao Instance => instance ??= new StatusDao();

    /// <summary>
    ///     Load all data from disk files.
    /// </summary>
    protected override void LoadAll()
    {
        Log.Info("Load items..");

        // Statuses
        Save(new SicknessData(Identifiers.Statuses.SICKNESS_ID)
        {
            Name = "Sickness",
            Description = "I'm not feeling... well",
            Duration = 60
        });

        Save(new RestrainedData(Identifiers.Statuses.RESTRAINED_ID)
        {
            Name = "Restrained",
            Description = "I'm innocent, I swear !",
            Duration = 0
        });

        Log.Info($"{All.Count} items charged !");
    }
}