using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Player.Roles;

namespace ssl.Player
{
    public class RoleDistributor
    {
        private HashSet<MainPlayer> players;
        public Scenario Scenario;

        public RoleDistributor(Scenario scenario, HashSet<MainPlayer> players)
        {
            this.Scenario = scenario;
            this.players = players;
            Log.Info("[RoleDistributor] Remove role for " + this.players.Count);
            foreach (MainPlayer mainPlayer in this.players)
            {
                mainPlayer.RoleHandler.AssignRole(null);
            }
        }

        public void Distribute()
        {
            Log.Info($"[RoleDistributor] Starting to distribute roles for {players.Count} players..");
            //Get constraints
            List<ScenarioConstraint> constraints = Scenario.GetScenarioConstraint(players.Count);
            Log.Info($"[RoleDistributor] {constraints.Count} constraints finded");
            //Fulfill constraits
            foreach (ScenarioConstraint constraint in constraints)
            {
                Log.Info($"[RoleDistributor] Constraint {constraint} have to be fullfilled");
                if (!ConstraintFullfilled(constraint) && FulfillConstraint(constraint))
                {
                    Log.Info($"[RoleDistributor] Constraint {constraint} is fulfilled");
                }
                else
                {
                    Log.Warning($"[RoleDistributor] Constraint {constraint} is not fulfilled, something wrong");
                }
            }

            Log.Info($"[RoleDistributor] {constraints.Count} Constraints are treated");
            Log.Info($"[RoleDistributor] {GetPlayersWithoutRole().Count} Not have any role");
            foreach (MainPlayer player in GetPlayersWithoutRole())
            {
                Log.Info($"[RoleDistributor] Give a role to {player}..");
                if (GivePreferedRoles(player))
                {
                    Log.Info($"[RoleDistributor] Player {player} get the role {player.RoleHandler.Role}");
                }
                else
                {
                    Log.Warning($"[RoleDistributor] Constraint {player} get Assistant by default");
                }
            }

            //TODO Give roles with their preferences
            if (GetPlayersWithoutRole().Count > 0)
            {
                Log.Error($"[RoleDistributor] Everyplayer don't have a role");
                throw new Exception();
            }
        }


        private ScenarioConstraint GetConstraint(Role role)
        {
            foreach (ScenarioConstraint scenarioConstraint in Scenario.GetScenarioConstraint(players.Count))
            {
                if (scenarioConstraint.Role.Equals(role)) return scenarioConstraint;
            }

            return null;
        }

        private bool ConstraintFullfilled(ScenarioConstraint constraint)
        {
            int min = constraint.Min;
            int current = GetPlayersWithRole().Count(player => player.RoleHandler.Role.Equals(constraint.Role));
            return current >= min;
        }

        private Dictionary<Role, float> GetPreferencesWithConstraints(Dictionary<Role, float> preferences)
        {
            Dictionary<Role, float> returnedPreferences = new();
            foreach ((Role role, float preference) in preferences)
            {
                ScenarioConstraint constraint = GetConstraint(role);
                if (constraint == null || CountRole(role) < GetConstraint(role).Max)
                {
                    returnedPreferences.Add(role, preference);
                }
                else
                {
                    returnedPreferences.Add(role, 0);
                }
            }

            return returnedPreferences;
        }

        private List<MainPlayer> GetPlayersWithoutRole()
        {
            List<MainPlayer> playersWithoutRoles = new();
            foreach (MainPlayer mainPlayer in players)
            {
                if (mainPlayer.RoleHandler.Role == null) playersWithoutRoles.Add(mainPlayer);
            }

            return playersWithoutRoles;
        }

        private int CountRole(Role role)
        {
            return GetPlayersWithRole(role).Count;
        }

        private IEnumerable<MainPlayer> GetPlayersWithRole()
        {
            return players.Where(mainPlayer => mainPlayer.RoleHandler.Role != null).ToList();
        }

        private List<MainPlayer> GetPlayersWithRole(Role role)
        {
            List<MainPlayer> playersWithRoles = new();
            foreach (MainPlayer mainPlayer in players)
            {
                if (role.Equals(mainPlayer.RoleHandler.Role)) playersWithRoles.Add(mainPlayer);
            }

            return playersWithRoles;
        }

        /// <summary>
        /// Give a role to the player
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>If false, the player have the default assistant role</returns>
        private bool GivePreferedRoles(MainPlayer player)
        {
            //Get preferences but remove roles with constraint max reached
            Dictionary<Role, float> preferences =
                GetPreferencesWithConstraints(player.RoleHandler.GetPreferencesNormalised());
            float total = preferences.Values.Sum();
            //If you have 0 preferences, you are assistant
            if (total <= 0f)
            {
                player.RoleHandler.AssignRole(new Assistant());
                return false;
            }

            Random random = new();
            float res = (float)random.NextDouble() * total;
            //Search the player with the role
            total = 0;
            foreach ((Role role, float preference) in preferences)
            {
                total += preference;
                if (res < total)
                {
                    player.RoleHandler.AssignRole(role);
                    return true;
                }
            }

            player.RoleHandler.AssignRole(new Assistant());
            return false;
        }


        private bool FulfillConstraint(ScenarioConstraint constraint)
        {
            //Add the preference of the player to the preferencesOfRole for this role
            Dictionary<MainPlayer, float> preferencesOfRole = GetPlayersWithoutRole()
                .ToDictionary(mainPlayer => mainPlayer,
                    mainPlayer => mainPlayer.RoleHandler.GetPreferencesNormalised()[constraint.Role]);
            //Get the total of preferences
            float total = preferencesOfRole.Values.Sum();
            //If nobody fulfill the constraint, make everyone want the role.
            if (total <= 0f)
            {
                total = preferencesOfRole.Count;
                foreach (MainPlayer mainPlayer in preferencesOfRole.Keys)
                {
                    preferencesOfRole[mainPlayer] = 1;
                }
            }

            //Random picker
            Random random = new();
            float res = (float)random.NextDouble() * total;
            //Search the player with the role
            total = 0;
            foreach ((MainPlayer player, float preference) in preferencesOfRole)
            {
                total += preference;
                if (res < total)
                {
                    player.RoleHandler.AssignRole(constraint.Role);
                    return true;
                }
            }

            return false;
        }
    }
}
