using System.Collections.Generic;
using ssl.Data;

namespace ssl.Dao;

public class ScenarioDao : LocalDao<ScenarioData>
{
    protected override Dictionary<string, ScenarioData> All { get; set; }
    
    /// <summary>
    /// Load all scenario from disk files.
    /// </summary>
    protected override void LoadAll()
    {
        Log.Info("Load scenarios..");
        
        Log.Info($"{All.Count} scenarios charged !");
    }
}
