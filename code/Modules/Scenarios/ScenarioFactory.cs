using System.Collections.Generic;
using ssl.Dao;
using ssl.Data;
using ssl.Factories;

namespace ssl.Modules.Scenarios;

public sealed class ScenarioFactory : IFactory<Scenario>
{
	private static ScenarioFactory instance;
	private readonly ScenarioDao scenarioDao = new();

	private ScenarioFactory()
	{
	}

	public static ScenarioFactory Instance => instance ??= new ScenarioFactory();

	public Scenario Create( string id )
	{
		ScenarioData scenarioData = scenarioDao.FindById(id);
		string scenarioType = scenarioData.GetTypeId();
			
		Scenario scenario;
		switch (scenarioType)
		{
			default:
				scenario = new Scenario(new Dictionary<int, List<ScenarioConstraint>>());
				break;
		}
		
		return scenario;
	}
}
