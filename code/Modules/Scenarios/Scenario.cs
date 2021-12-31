using System.Collections.Generic;
using System.Linq;

namespace ssl.Modules.Scenarios;

public class Scenario
{
    public string Id { get; set; }

    /// <summary>
    ///     List of constraints per players palliers.
    /// </summary>
    public Dictionary<int, List<ScenarioConstraint>> Constraints { get; set; }

    public List<ScenarioConstraint> GetScenarioConstraint(int count)
    {
        List<ScenarioConstraint> constraintToReturn = new();
        foreach ((int numberOfPlayers, List<ScenarioConstraint> currentConstraintsList) in Constraints)
        {
            if (numberOfPlayers > count) return constraintToReturn;

            constraintToReturn = currentConstraintsList;
        }

        return constraintToReturn;
    }

    public int MinRole(int playerCount, string role)
    {
        foreach (ScenarioConstraint scenarioConstraint in GetScenarioConstraint(playerCount)
                     .Where(scenarioConstraint => scenarioConstraint.RoleId.Equals(role)))
        {
            return scenarioConstraint.Min;
        }

        return -1;
    }

    public int MaxRole(int playerCount, string role)
    {
        foreach (ScenarioConstraint scenarioConstraint in GetScenarioConstraint(playerCount)
                     .Where(scenarioConstraint => scenarioConstraint.RoleId.Equals(role)))
        {
            return scenarioConstraint.Max;
        }

        return -1;
    }
}