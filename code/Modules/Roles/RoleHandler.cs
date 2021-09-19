using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Modules.Saves;
using ssl.Player;

namespace ssl.Modules.Roles
{
    public partial class RoleHandler : NetworkedEntityAlwaysTransmitted
    {
        
        private RolesPrefencesSaver saver;

        private static Dictionary<RolePreferenceType, int> rolesFactors = new()
        {
            { RolePreferenceType.Never, 0 },
            { RolePreferenceType.Low, 1 },
            { RolePreferenceType.Medium, 5 },
            { RolePreferenceType.High, 25 },
            { RolePreferenceType.Always, 125 },
        };

        [Net, Local, OnChangedCallback] private List<RolePreference> RolePreferences { get; set; }
        [Net, Local, OnChangedCallback] private string A { get; set; }
        
        private MainPlayer player;

        public RoleHandler(MainPlayer player)
        {
            this.player = player;
            saver = new RolesPrefencesSaver();
            RolePreferences = new List<RolePreference>();
            InitRolePreferences();
            if(Host.IsClient) LoadRoles();
        }
        public Role Role { get; private set; }
        
        [ServerCmd("select_preference_role")]
        public static void SelectPreference(string roleId, RolePreferenceType preferenceType)
        {
            MainPlayer player = (MainPlayer)ConsoleSystem.Caller.Pawn;
            player.RoleHandler?.SetPreference(Role.All[roleId], preferenceType);
            player.RoleHandler.A = "A";
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
        
        private void LoadRoles()
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
        
        

        public void Clear()
        {
            AssignRole(null);
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
            RolePreference rolePreference = null;
            foreach (RolePreference preference in RolePreferences)
            {
                if (role.Equals(preference.Role)) rolePreference = preference;
            }
            
            if (null == rolePreference)
            {
                RolePreferences.Add(new RolePreference(role, preferenceType));
            }
            else
            {
                rolePreference.Preference = preferenceType;
            }
            Log.Info("change");
        }

        /// <summary>
        /// When player spawn with role
        /// </summary>
        public void SpawnRole()
        {
            Role?.OnSpawn(player);
        }
        
        private void OnRolePreferencesChanged()
        {
            Log.Info("Change client preferences");
            Log.Info("TEST");
            Host.AssertClient();
            saver.Save(RolePreferences);
        }
        
        private void OnAChanged()
        {
            Log.Info("Change client preferences");
            Log.Info("TEST");
        }
    }
}