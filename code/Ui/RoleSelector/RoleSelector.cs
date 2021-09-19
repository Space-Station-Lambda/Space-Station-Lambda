using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using ssl.Modules.Roles;
using ssl.Modules.Rounds;
using ssl.Modules.Saves;

namespace ssl.Ui.RoleSelector
{
    /// <summary>
    /// Role selector allow player to select a role
    /// </summary>
    public class RoleSelector : Panel
    {
        private readonly List<RoleSlot> roleSlots = new();

        public RoleSelector()
        {
            StyleSheet.Load("Ui/RoleSelector/RoleSelector.scss");
            SetClass("active", true);
            foreach ((string id, Role role) in Role.All)
            {
                RoleSlot slot = new(role, this);
                roleSlots.Add(slot);
                break;
            }
        }

        public override void Tick()
        {
            base.Tick();
            RefreshSlots();
            BaseRound currentRound = Gamemode.Instance.RoundManager?.CurrentRound;
            if (null != currentRound)
            {
                SetClass("active", currentRound is PreRound);
                SetClass("hidden", currentRound is not PreRound);
            }
        }
        
        private void RefreshSlots()
        {
            foreach (RoleSlot slot in roleSlots)
            {
                slot.Refresh();
            }
        }

    }
}