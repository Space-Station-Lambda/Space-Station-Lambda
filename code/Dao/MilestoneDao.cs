using System.Collections.Generic;
using ssl.Data;

namespace ssl.Dao;

public class MilestoneDao :  LocalDao<MilestoneData>
{
	protected override Dictionary<string, MilestoneData> All { get; set; }
	
	protected override void LoadAll()
	{
		Log.Info("Load milestones..");
		
		Save(new MilestoneData( "milestone_1" ));
		
		Log.Info($"{All.Count} Charged !");
	}
}
