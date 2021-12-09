using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Modules.Roles.Instances;
using ssl.Modules.Saves;
using ssl.Player;

namespace ssl.Modules.Roles;

public partial class RoleHandler : EntityComponent<SslPlayer>
{
	private static readonly Dictionary<RolePreferenceType, int> RolesFactors = new()
	{
		{RolePreferenceType.Never, 0},
		{RolePreferenceType.Low, 1},
		{RolePreferenceType.Medium, 5},
		{RolePreferenceType.High, 25},
		{RolePreferenceType.Always, 125}
	};

	private readonly RolesPrefencesSaver saver;

	public RoleHandler()
	{
		if ( Host.IsClient )
		{
			saver = new RolesPrefencesSaver();
			LoadPreferences();
		}
	}
	
	[Net] private Dictionary<string, RolePreferenceType> RolePreferences { get; set; }
	
	public Role Role { get; private set; }

	[ServerCmd("select_role_preference")]
	public static void SelectPreference( string roleId, RolePreferenceType preferenceType )
	{
		SslPlayer sslPlayer = (SslPlayer)ConsoleSystem.Caller.Pawn;
		sslPlayer.RoleHandler?.SetPreference(roleId, preferenceType);
	}

	[ClientCmd("save_role_preferences")]
	public static void SaveRolePreferences()
	{
		SslPlayer sslPlayer = (SslPlayer)Local.Pawn;
		sslPlayer.RoleHandler.SavePreferences();
	}

	public RolePreferenceType GetPreference( string roleId )
	{
		return RolePreferences[roleId];
	}

	private void InitRolePreferences()
	{
		foreach ( string roleId in RoleFactory.Instance.GetAllIds() )
		{
			RolePreferences[roleId] = RolePreferenceType.Never;
		}
	}

	private void LoadPreferences()
	{
		Host.AssertClient();
		if ( saver.IsSaved )
		{
			var rolePreferences = saver.Load();
			foreach ( (string roleId, RolePreferenceType preference) in rolePreferences )
			{
				ConsoleSystem.Run("select_preference_role", roleId, preference);
			}
		}
	}

	public Dictionary<string, float> GetPreferencesNormalised()
	{
		int total = RolePreferences.Sum(rolePreference => RolesFactors[rolePreference.Value]);

		Dictionary<string, float> normalisedPreferences = new();

		foreach ( string roleId in RolePreferences.Keys )
		{
			if ( total == 0 )
			{
				normalisedPreferences[roleId] = 0f;
			}
			else
			{
				normalisedPreferences[roleId] = (float)RolesFactors[RolePreferences[roleId]] / total;
			}
		}

		return normalisedPreferences;
	}

	public void AssignRole( Role role )
	{
		Role?.OnUnassigned(Entity);
		Role = role;
		Role?.OnAssigned(Entity);
	}
	
	public void AssignRole( string roleId )
	{
		AssignRole(RoleFactory.Instance.Create(roleId));
	}
	
	public void ClearRole()
	{
		Role?.OnUnassigned(Entity);
		Role = null;
	}

	public void SetPreference( string roleId, RolePreferenceType preferenceType )
	{
		Host.AssertServer();
		RolePreferences[roleId] = preferenceType;
	}

	/// <summary>
	///     When player spawn with role
	/// </summary>
	public void SpawnRole()
	{
		Role?.OnSpawn(Entity);
	}

	public void SavePreferences()
	{
		Host.AssertClient();
		saver.Save(RolePreferences);
	}

	protected override void OnActivate()
	{
		if ( Host.IsServer )
		{
			InitRolePreferences();
		}
	}
}
