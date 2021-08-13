﻿using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Modules.Roles.Types.Jobs;
using ssl.Player;

namespace ssl.Modules.Roles
{
    public class RoleHandler : NetworkComponent
    {
        private static Dictionary<RolePreference, int> rolesFactors = new()
        {
            { RolePreference.Never, 0 },
            { RolePreference.Low, 1 },
            { RolePreference.Medium, 5 },
            { RolePreference.High, 25 },
            { RolePreference.Always, 125 },
        };

        private readonly Dictionary<Role, RolePreference> rolePreferences;
        private MainPlayer player;

        public RoleHandler(MainPlayer player)
        {
            rolePreferences = new Dictionary<Role, RolePreference>();
            this.player = player;
            foreach (Role role in Role.All.Values)
            {
                rolePreferences.Add(role, RolePreference.Never);
            }
        }

        [Net] public Role Role { get; private set; }

        [ServerCmd("select_preference_role")]
        public static void SelectPreference(string roleId, RolePreference preference)
        {
            RoleHandler target = ((MainPlayer)ConsoleSystem.Caller.Pawn).RoleHandler;
            target?.SetPreference(Role.All[roleId], preference);
        }

        public void Clear()
        {
            AssignRole(null);
        }

        public Dictionary<Role, float> GetPreferencesNormalised()
        {
            int total = 0;
            foreach (RolePreference rolePreferencesValue in rolePreferences.Values)
            {
                total += rolesFactors[rolePreferencesValue];
            }

            Dictionary<Role, float> normalisedPreferences = new();

            foreach ((Role key, RolePreference value) in rolePreferences)
            {
                if (total == 0) normalisedPreferences[key] = 0f;
                else normalisedPreferences[key] = (float)rolesFactors[value] / total;
            }

            return normalisedPreferences;
        }

        public void AssignRole(Role role)
        {
            Role?.OnUnassigned(player);
            Role = role;
            Role?.OnAssigned(player);
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

        /// <summary>
        /// Get a random role from the preferences
        /// </summary>
        /// <returns></returns>
        public Role GetRandomRoleFromPreferences()
        {
            int totalPoints = 0;
            foreach ((Role role, RolePreference value) in rolePreferences)
            {
                totalPoints += rolesFactors[value];
            }

            Random rnd = new();
            int res = rnd.Next(totalPoints);
            Log.Info("Random number for pick is " + res + " /" + totalPoints);
            totalPoints = 0;
            foreach ((Role role, RolePreference value) in rolePreferences)
            {
                Log.Info("Add " + rolesFactors[value] + " for role " + role);
                totalPoints += rolesFactors[value];
                if (res < totalPoints) return role;
            }

            return new Assistant();
        }

        /// <summary>
        /// When player spawn with role
        /// </summary>
        public void Init()
        {
            if (Role != null)
            {
                player.ClothesHandler.AttachClothes(Role.Clothing);
                Role.OnSpawn(player);
            }
        }
    }
}