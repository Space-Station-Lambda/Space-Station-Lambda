using System.Collections.Generic;
using ssl.Data;

namespace ssl.Dao;

public class RoleDao : LocalDao<RoleData>
{
	protected override Dictionary<string, RoleData> All { get; set; }
	protected override void LoadAll()
	{
		throw new System.NotImplementedException();
	}
}
