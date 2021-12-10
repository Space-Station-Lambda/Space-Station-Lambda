using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;

namespace ssl.Modules.Scenarios;

public class ScenarioDao : LocalDao<ScenarioData>
{
	protected override void LoadAll()
	{
		Log.Info("Loading scenarios...");

		Save(new ScenarioData(Identifiers.Basic)
		{
			Constraints = new Dictionary<int, List<ScenarioConstraint>>()
			{
				{
					2,
					new List<ScenarioConstraint>
					{
						new($"{Identifiers.Role}{Identifiers.Separator}{Identifiers.Guard}", 1, 1)
					}
				},
				{
					3, 
					new List<ScenarioConstraint>
					{
						new($"{Identifiers.Role}{Identifiers.Separator}{Identifiers.Traitor}", 1, 1),
						new($"{Identifiers.Role}{Identifiers.Separator}{Identifiers.Guard}", 2, 3)
					}
				}
			}
		});
		Log.Info("Scenarios loaded.");
	}
}
