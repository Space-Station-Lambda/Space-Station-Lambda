using System.Collections.Generic;
using System.Linq;
using ssl.Modules.Roles;

namespace ssl.Modules.Scenarios;

public class Scenario
{
	private readonly Dictionary<int, List<ScenarioConstraint>> constraintsLists;

	public Scenario( Dictionary<int, List<ScenarioConstraint>> constraintsLists )
	{
		this.constraintsLists = constraintsLists;
	}

	public List<ScenarioConstraint> GetScenarioConstraint( int count )
	{
		List<ScenarioConstraint> constraintToReturn = new();
		foreach ( (int numberOfPlayers, var currentConstraintsList) in constraintsLists )
		{
			if ( numberOfPlayers > count )
			{
				return constraintToReturn;
			}

			constraintToReturn = currentConstraintsList;
		}

		return constraintToReturn;
	}

	public int MinRole( int playerCount, Role role )
	{
		foreach ( ScenarioConstraint scenarioConstraint in GetScenarioConstraint(playerCount)
			         .Where(scenarioConstraint => scenarioConstraint.Role.Equals(role)) )
		{
			return scenarioConstraint.Min;
		}

		return -1;
	}

	public int MaxRole( int playerCount, Role role )
	{
		foreach ( ScenarioConstraint scenarioConstraint in GetScenarioConstraint(playerCount)
			         .Where(scenarioConstraint => scenarioConstraint.Role.Equals(role)) )
		{
			return scenarioConstraint.Max;
		}

		return -1;
	}
}
