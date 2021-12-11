using System.Collections.Generic;
using ssl.Commons;

namespace ssl.Modules.Scenarios;

public sealed class ScenarioFactory : IFactory<Scenario>
{
	private static ScenarioFactory instance;

	private ScenarioFactory()
	{
	}

	public static ScenarioFactory Instance => instance ??= new ScenarioFactory();

	public Scenario Create(string id)
	{
		ScenarioData scenarioData = ScenarioDao.Instance.FindById(id);

		Scenario scenario = scenarioData switch
		{
			_ => new Scenario
			{
				Id = scenarioData.Id,
				Constraints = scenarioData.Constraints
			}
		};

		return scenario;
	}
}
