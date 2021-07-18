using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.ScreenShake;

namespace ssl.Player.Roles
{
    public class RoleHandler : NetworkComponent
    {
        private Dictionary<RolePreference, int> rolesFactors = new()
        {
            { RolePreference.Never, 0 },
            { RolePreference.Low, 1 },
            { RolePreference.Medium, 5 },
            { RolePreference.High, 25 },
            { RolePreference.Always, 125 },
        };

        private readonly Dictionary<Role, RolePreference> rolePreferences;
        public RoleHandler()
        {
            rolePreferences = new Dictionary<Role, RolePreference>();
        }


        [Net] public Role Role { get; private set; }
        
        [ServerCmd("select_preference_role")]
        public static void SelectPreference(string roleId, RolePreference preference)
        {
            RoleHandler target = ((MainPlayer)ConsoleSystem.Caller.Pawn).RoleHandler;
            target?.SetPreference(GetRoleById(roleId), preference);
        }
        
        public void AssignRole(Role role)
        {
            Log.Info($"Role {role} assigned");
            Role = role;
        }

        public void SetPreference(Role role, RolePreference preference)
        {
            if (!rolePreferences.ContainsKey(role))
            {
                rolePreferences.Add(role, preference);
            }
            else
            {
                rolePreferences[role] = preference;
            }
        }

        public void AssignRandomRole()
        {
            AssignRole(GetRandomRoleFromPreferences());
        }
        
        /// <summary>
        /// Get a random role from the preferences
        /// </summary>
        /// <returns></returns>
        public Role GetRandomRoleFromPreferences()
        {
            int totalPoints = rolePreferences.Sum(rolePreference => rolesFactors[rolePreference.Value]);
            System.Random rnd = new();
            int res = rnd.Next(totalPoints);
            totalPoints = 0;
            foreach ((Role role, RolePreference value) in rolePreferences)
            {
                if (res <= totalPoints) return role;
                totalPoints += rolesFactors[value];
            }
            return new Assistant();
        }
        /// <summary>
        /// TODO Role registery
        /// </summary>
        /// <returns></returns>
        private static Role GetRoleById(string id)
        {
            switch (id)
            {
                case "assistant":
                    return new Assistant();
                case "captain":
                    return new Captain();
                case "engineer":
                    return new Engineer();
                case "ghost":
                    return new Ghost();
                case "guard":
                    return new Guard();
                case "janitor":
                    return new Janitor();
                case "mechanic":
                    return new Mechanic();
                case "scientist":
                    return new Scientist();
                case "traitor":
                    return new Traitor();
            }

            throw new Exception($"This id {id} don't exist");
        }
    }
}