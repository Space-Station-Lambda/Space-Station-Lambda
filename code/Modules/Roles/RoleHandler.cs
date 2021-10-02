using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Modules.Roles.Types.Jobs;
using ssl.Modules.Saves;
using ssl.Player;

namespace ssl.Modules.Roles
{
    public partial class RoleHandler : BaseNetworkable
    {

        private static Dictionary<RolePreferenceType, int> rolesFactors = new()
        {
            { RolePreferenceType.Never, 0 },
            { RolePreferenceType.Low, 1 },
            { RolePreferenceType.Medium, 5 },
            { RolePreferenceType.High, 25 },
            { RolePreferenceType.Always, 125 },
        };

        private MainPlayer player;
        private readonly RolesPrefencesSaver saver;

        public RoleHandler()
        {
            saver = new RolesPrefencesSaver();
            if(Host.IsClient) LoadPreferences();
        }

        public RoleHandler(MainPlayer player) : this()
        {
            this.player = player;
            RolePreferences = new List<RolePreference>();
            InitRolePreferences();
        }
        
        [Net] private List<RolePreference> RolePreferences { get; set; }

        public Role Role { get; private set; }
        
        [ServerCmd("select_preference_role")]
        public static void SelectPreference(string roleId, RolePreferenceType preferenceType)
        {
            MainPlayer player = (MainPlayer)ConsoleSystem.Caller.Pawn;
            player.RoleHandler?.SetPreference(Role.All[roleId], preferenceType);
        }

        public RolePreferenceType GetPreference(Role role)
        {
            return RolePreferences.Where(rolePreference => rolePreference.Role.Equals(role)).Select(rolePreference => rolePreference.Preference).FirstOrDefault();
        }
        
        private void InitRolePreferences()
        {
            foreach (Role role in Role.All.Values)
            {
                RolePreferences.Add(new RolePreference(role, RolePreferenceType.Never));
            }
        }
        
        private void LoadPreferences()
        {
            Host.AssertClient();
            if (saver.IsSaved)
            {
                List<RolePreference> rolePreferences = saver.Load();
                foreach (RolePreference preference in rolePreferences)
                {
                    ConsoleSystem.Run("select_preference_role", preference.Role.Id, preference.Preference);
                }
            }
        }

        public Dictionary<Role, float> GetPreferencesNormalised()
        {
            int total = RolePreferences.Sum(rolePreference => rolesFactors[rolePreference.Preference]);

            Dictionary<Role, float> normalisedPreferences = new();

            foreach (RolePreference preference in RolePreferences)
            {
                if (total == 0) normalisedPreferences[preference.Role] = 0f;
                else normalisedPreferences[preference.Role] = (float)rolesFactors[preference.Preference] / total;
            }

            return normalisedPreferences;
        }

        public void AssignRole(Role role)
        {
            Role?.OnUnassigned(player);
            Role = role;
            Role?.OnAssigned(player);
        }
        
        public void SetPreference(Role role, RolePreferenceType preferenceType)
        {
            Host.AssertServer();
            RolePreference rolePreference = null;
            foreach (RolePreference preference in RolePreferences.Where(preference => role.Equals(preference.Role)))
            {
                rolePreference = preference;
            }
            
            if (null == rolePreference)
            {
                RolePreferences.Add(new RolePreference(role, preferenceType));
            }
            else
            {
                rolePreference.Preference = preferenceType;
            }
            // SavePreferences();
        }

        /// <summary>
        /// When player spawn with role
        /// </summary>
        public void SpawnRole()
        {
             Role?.OnSpawn(player);
        }
        
        // [ClientRpc]
        // private void SavePreferences()
        // {
        //     Host.AssertClient();
        //     saver.Save(RolePreferences);
        // }
    }
}