using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;

namespace ssl.Modules.Scenarios;

public class ScenarioData : BaseData
{
	public ScenarioData(string id) : base(id)
	{
	}

	public Dictionary<int, List<ScenarioConstraint>> Constraints { get; set; }
}
