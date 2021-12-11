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
		string scenarioType = scenarioData.GetTypeId();
			
		Scenario scenario;
		switch (scenarioType)
		{
			default:
				scenario = new Scenario
				{
					Id = scenarioData.Id,
					Constraints = scenarioData.Constraints
				};
				break;
		}
		
		return scenario;
	}
}
