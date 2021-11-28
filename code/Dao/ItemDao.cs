using System.Collections.Generic;
using ssl.Data;

namespace ssl.Dao;

public class ItemDao : LocalDao<ItemData>
{
    protected override Dictionary<string, ItemData> All { get; set; }
    
    /// <summary>
    /// Load all data from disk files.
    /// </summary>
    protected override void LoadAll()
    {
        throw new System.NotImplementedException();
    }
}