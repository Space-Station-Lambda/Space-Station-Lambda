using System.Collections.Generic;
using ssl.Data;

namespace ssl.Modules.Scenarios;

public class ScenarioData : BaseData
{
	public ScenarioData(string id) : base($"{Identifiers.Scenario}{Identifiers.Separator}{id}")
	{
	}

	public Dictionary<int, List<ScenarioConstraint>> Constraints { get; set; }
}
