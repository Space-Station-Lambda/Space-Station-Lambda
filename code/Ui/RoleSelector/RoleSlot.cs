﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Roles;
using ssl.Modules.Roles.Instances;
using ssl.Player;

namespace ssl.Ui.RoleSelector;

/// <summary>
///     Role icon to be chosed
/// </summary>
public class RoleSlot : Panel
{
    private readonly string roleId;
    private RolePreferenceType currentSelected;

    public RoleSlot(Role role, Panel parent) : this(role)
    {
        StyleSheet.Load("Ui/RoleSelector/RoleSlot.scss");
        Parent = parent;
        AddEventListener("onclick", Select);
    }

    public RoleSlot(Role role)
    {
        StyleSheet.Load("Ui/RoleSelector/RoleSlot.scss");
        roleId = role.Id;

        Panel inside = Add.Panel("inside");
        {
            Panel roleLabel = inside.Add.Panel("roleLabel");
            {
                roleLabel.Add.Label(role.Name, "role-name");
            }
        }
    }

    public void Refresh()
    {
        RolePreferenceType newPreferenceType = ((SslPlayer) Local.Pawn).RoleHandler.GetPreference(roleId);

        if (currentSelected == newPreferenceType) return;

        currentSelected = newPreferenceType;
        SetClass("selected", currentSelected == RolePreferenceType.Medium);
    }

    /// <summary>
    ///     Select the role and setrole to the client
    /// </summary>
    public void Select()
    {
        ConsoleSystem.Run("select_role_preference", roleId,
            currentSelected == RolePreferenceType.Medium ? RolePreferenceType.Never : RolePreferenceType.Medium);
        ConsoleSystem.Run("save_role_preferences");
    }
}