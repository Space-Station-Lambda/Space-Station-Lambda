using System.Collections.Generic;
using ssl.Modules.Roles;
using ssl.Modules.Roles.Types.Antagonists;
using ssl.Modules.Roles.Types.Jobs;
using ssl.Modules.Scenarios;

namespace ssl.Modules.Rounds
{
    public class PreRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 10;

        public override BaseRound Next()
        {
            return new InProgressRound();
        }

        protected override void OnTimeUp()
        {
            AssignRoles();
            base.OnTimeUp();
        }

        private void AssignRoles()
        {
            Scenario scenario = new(
                new Dictionary<int, List<ScenarioConstraint>>
                {
                    {
                        2, new List<ScenarioConstraint>
                        {
                            new(new Guard(), 1, 1)
                        }
                    },
                    {
                        3, new List<ScenarioConstraint>
                        {
                            new(new Traitor(), 1, 1),
                            new(new Guard(), 2, 3)
                        }
                    }
                });
            RoleDistributor distributor = new(scenario, Players);
            distributor.Distribute();
        }
    }
}