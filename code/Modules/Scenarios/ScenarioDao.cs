using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;

namespace ssl.Modules.Scenarios;

public class ScenarioDao : LocalDao<ScenarioData>
{
    private static ScenarioDao instance;

    private ScenarioDao() { }

    public static ScenarioDao Instance => instance ??= new ScenarioDao();

    protected override void LoadAll()
    {
        Log.Info("Loading scenarios...");

        Save(new ScenarioData(Identifiers.BASE_SCENARIO_ID)
        {
            Constraints = new Dictionary<int, List<ScenarioConstraint>>
            {
                {
                    2,
                    new List<ScenarioConstraint>
                    {
                        new(Identifiers.GUARD_ID, 1, 1)
                    }
                },
                {
                    3,
                    new List<ScenarioConstraint>
                    {
                        new(Identifiers.TRAITOR_ID, 1, 1),
                        new(Identifiers.GUARD_ID, 2, 3)
                    }
                }
            }
        });

        Log.Info("Scenarios loaded.");
    }
}