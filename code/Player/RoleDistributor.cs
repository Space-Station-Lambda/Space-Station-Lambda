using System;
using System.Collections.Generic;
using System.Text;
using Sandbox;
using ssl.Player.Roles;

namespace ssl.Player
{
    public class RoleDistributor
    {
        public Scenario Scenario = Scenario.BasicScenario;

        private List<MainPlayer> players;

        public RoleDistributor(List<MainPlayer> players)
        {
            this.players = players;
        }

        public void Distribute()
        {
            //Get constraints
            List<ScenarioConstraint> constraints = Scenario.GetScenarioConstraint(players.Count);
            foreach (ScenarioConstraint constraint in constraints)
            {
                FulfillConstraint();
            }
            //TODO Give player roles with their preferences
        }

        public void FulfillConstraint(ScenarioConstraint constraint)
        {
            Dictionary<MainPlayer, float> normalisedPreferencesOfRole = new();
            foreach (MainPlayer mainPlayer in players)
            {
                
            }
        }
    }
}