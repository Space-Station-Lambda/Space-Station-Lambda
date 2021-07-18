using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;

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
            Log.Info("Random number for pick is " + res + " /" + totalPoints);
            totalPoints = 0;
            
            foreach ((Role role, RolePreference value) in rolePreferences)
            {
                totalPoints += rolesFactors[value];
                if (res <= totalPoints) return role;
            }
            return new Assistant();
        }
        /// <summary>
        /// TODO Role registery
        /// </summary>
        /// <returns></returns>
        private static Role GetRoleById(string id)
        {
            return id switch
            {
                "assistant" => new Assistant(),
                "captain" => new Captain(),
                "engineer" => new Engineer(),
                "ghost" => new Ghost(),
                "guard" => new Guard(),
                "janitor" => new Janitor(),
                "mechanic" => new Mechanic(),
                "scientist" => new Scientist(),
                "traitor" => new Traitor(),
                _ => throw new Exception($"This id {id} don't exist")
            };
        }
    }
}