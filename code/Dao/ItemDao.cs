using System.Collections.Generic;
using ssl.Data;

namespace ssl.Dao;

public class ItemDao : AbstractDao<ItemData>
{
    protected override Dictionary<string, ItemData> Datas { get; set; }
    
    /// <summary>
    /// Load all data from disk files.
    /// </summary>
    protected override void LoadAll()
    {
        throw new System.NotImplementedException();
    }
}