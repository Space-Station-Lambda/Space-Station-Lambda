using System.Collections.Generic;
using ssl.Commons;

namespace ssl.Modules.Skills;

public class SkillDao : LocalDao<SkillData>
{
    protected override Dictionary<string, SkillData> All { get; set; }
    
    /// <summary>
    ///     Load all skills data from disk files.
    /// </summary>
    protected override void LoadAll()
    {
        Log.Info("Load skills..");
        
        Log.Info($"{All.Count} skills charged !");
    }
}
