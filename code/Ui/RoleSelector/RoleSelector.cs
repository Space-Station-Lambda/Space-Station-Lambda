using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using ssl.Modules.Roles;
using ssl.Modules.Roles.Types.Antagonists;
using ssl.Modules.Roles.Types.Jobs;
using ssl.Modules.Rounds;

namespace ssl.Ui.RoleSelector
{
    /// <summary>
    /// Role selector allow player to select a role
    /// </summary>
    public class RoleSelector : Panel
    {
        private readonly Dictionary<RoleSelectorSlot, bool> rolesSelected = new();

        public RoleSelector()
        {
            StyleSheet.Load("Ui/RoleSelector/RoleSelector.scss");
            SetClass("active", true);
            rolesSelected.Add(new RoleSelectorSlot(new Assistant(), this), false);
            rolesSelected.Add(new RoleSelectorSlot(new Janitor(), this), false);
            rolesSelected.Add(new RoleSelectorSlot(new Scientist(), this), false);
            rolesSelected.Add(new RoleSelectorSlot(new Guard(), this), false);
            rolesSelected.Add(new RoleSelectorSlot(new Captain(), this), false);
            rolesSelected.Add(new RoleSelectorSlot(new Engineer(), this), false);
            rolesSelected.Add(new RoleSelectorSlot(new Traitor(), this), false);
            foreach (RoleSelectorSlot roleIcon in rolesSelected.Keys)
            {
                if (rolesSelected[roleIcon]) roleIcon.Select();
                roleIcon.AddEventListener("onclick", () => { Select(roleIcon); });
                ConsoleSystem.Run("select_preference_role", roleIcon.Role.Id, RolePreference.Never);
            }
            
            Gamemode.Instance.RoundManager.RoundStarted += OnRoundStarted;
        }

        /// <summary>
        /// Select a specific role
        /// </summary>
        /// <param name="roleSelectorSlot">Slot to select</param>
        public void Select(RoleSelectorSlot roleSelectorSlot)
        {
            if (rolesSelected[roleSelectorSlot])
            {
                roleSelectorSlot.Unselect();
                rolesSelected[roleSelectorSlot] = false;
            }
            else
            {
                roleSelectorSlot.Select();
                rolesSelected[roleSelectorSlot] = true;
            }
        }

        public void OnRoundStarted()
        {
            BaseRound currentRound = Gamemode.Instance.RoundManager.CurrentRound;
            SetClass("active", currentRound is PreRound);
            SetClass("hidden", currentRound is not PreRound);
        }

        
    }
}