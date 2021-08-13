using System.Collections.Generic;
using Sandbox;
using ssl.Player;
using ssl.Roles;

namespace ssl.Rounds
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
                    }},
                {
                    3, new List<ScenarioConstraint>
                {
                    new(new Traitor(), 1, 1),
                    new(new Guard(), 2, 3)
                }}
            });
            RoleDistributor distributor = new(scenario, Players);
            distributor.Distribute();
        }
    }
}