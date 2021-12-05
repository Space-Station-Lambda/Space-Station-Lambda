﻿using System;
using System.Collections.Generic;
using System.Linq;
using ssl.Modules.Roles.Types.Jobs;
using ssl.Modules.Scenarios;
using ssl.Player;

namespace ssl.Modules.Roles;

public class RoleDistributor
{
	public Role DefaultRole = new Assistant();
	private readonly IList<SslPlayer> players;
	public Scenario Scenario;

	public RoleDistributor( Scenario scenario, IList<SslPlayer> players )
	{
		Scenario = scenario;
		this.players = players;
		Log.Info("[RoleDistributor] Remove role for " + this.players.Count);
		foreach ( SslPlayer mainPlayer in this.players )
		{
			mainPlayer.RoleHandler.AssignRole(null);
		}
	}

	public void Distribute()
	{
		Log.Info($"[RoleDistributor] Starting to distribute roles for {players.Count} players..");
		//Get constraints
		var constraints = Scenario.GetScenarioConstraint(players.Count);
		Log.Info($"[RoleDistributor] {constraints.Count} constraints finded");
		//Fulfill constraits
		foreach ( ScenarioConstraint constraint in constraints )
		{
			Log.Info($"[RoleDistributor] Constraint {constraint} have to be fullfilled");
			if ( !ConstraintFullfilled(constraint) && FulfillConstraint(constraint) )
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
		foreach ( SslPlayer player in GetPlayersWithoutRole() )
		{
			Log.Info($"[RoleDistributor] Give a role to {player}..");
			if ( GivePreferredRole(player) )
			{
				Log.Info($"[RoleDistributor] Player {player} get the role {player.RoleHandler.Role}");
			}
			else
			{
				Log.Warning($"[RoleDistributor] Constraint {player} get the default role.");
			}
		}

		//TODO Give roles with their preferences
		if ( GetPlayersWithoutRole().Count > 0 )
		{
			Log.Error("[RoleDistributor] Everyplayer don't have a role");
			throw new Exception();
		}
	}


	private ScenarioConstraint GetConstraint( Role role )
	{
		return Scenario.GetScenarioConstraint(players.Count)
			.FirstOrDefault(scenarioConstraint => scenarioConstraint.Role.Equals(role));
	}

	private bool ConstraintFullfilled( ScenarioConstraint constraint )
	{
		int min = constraint.Min;
		int current = GetPlayersWithRole().Count(player => player.RoleHandler.Role.Equals(constraint.Role));
		//If the constraint is -1 or >= min
		return min < 0 || current >= min;
	}

	private Dictionary<Role, float> GetPreferencesWithConstraints( Dictionary<Role, float> preferences )
	{
		Dictionary<Role, float> returnedPreferences = new();
		foreach ( (Role role, float preference) in preferences )
		{
			ScenarioConstraint constraint = GetConstraint(role);
			//If the constraint is -1 or null or <= max
			if ( constraint == null || constraint.Max < 0 || CountRole(role) < constraint.Max )
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

	private List<SslPlayer> GetPlayersWithoutRole()
	{
		List<SslPlayer> playersWithoutRoles = new();
		foreach ( SslPlayer mainPlayer in players )
		{
			if ( mainPlayer.RoleHandler.Role == null )
			{
				playersWithoutRoles.Add(mainPlayer);
			}
		}

		return playersWithoutRoles;
	}

	private int CountRole( Role role )
	{
		return GetPlayersWithRole(role).Count;
	}

	private IEnumerable<SslPlayer> GetPlayersWithRole()
	{
		return players.Where(mainPlayer => mainPlayer.RoleHandler.Role != null).ToList();
	}

	private List<SslPlayer> GetPlayersWithRole( Role role )
	{
		return players.Where(mainPlayer => role.Equals(mainPlayer.RoleHandler.Role)).ToList();
	}

	/// <summary>
	///     Give a role to the player
	/// </summary>
	/// <param name="sslPlayer">The player</param>
	/// <returns>If false, the player have the default assistant role</returns>
	private bool GivePreferredRole( SslPlayer sslPlayer )
	{
		Role role = GetPreferedRole(sslPlayer);
		if ( role == null )
		{
			sslPlayer.RoleHandler.AssignRole(DefaultRole);
			return false;
		}

		sslPlayer.RoleHandler.AssignRole(role);
		return true;
	}

	/// <summary>
	///     Search for the availible prefed role
	/// </summary>
	/// <param name="sslPlayer">The player</param>
	/// <returns>null if the player can't have his prefered role</returns>
	public Role GetPreferedRole( SslPlayer sslPlayer )
	{
		Role roleToAssign = null;
		var preferences =
			GetPreferencesWithConstraints(sslPlayer.RoleHandler.GetPreferencesNormalised());
		float total = preferences.Values.Sum();
		//If you have 0 preferences, you are assistant
		if ( total <= 0f )
		{
			return null;
		}

		Random random = new();
		float res = (float)random.NextDouble() * total;
		//Search the player with the role
		total = 0;
		foreach ( (Role role, float preference) in preferences )
		{
			total += preference;
			if ( res < total )
			{
				roleToAssign = role;
				break;
			}
		}

		return roleToAssign;
	}


	private bool FulfillConstraint( ScenarioConstraint constraint )
	{
		//Add the preference of the player to the preferencesOfRole for this role
		var preferencesOfRole = GetPlayersWithoutRole()
			.ToDictionary(mainPlayer => mainPlayer,
				mainPlayer => mainPlayer.RoleHandler.GetPreferencesNormalised()[constraint.Role]);
		//Get the total of preferences
		float total = preferencesOfRole.Values.Sum();
		//If nobody fulfill the constraint, make everyone want the role.
		if ( total <= 0f )
		{
			total = preferencesOfRole.Count;
			foreach ( SslPlayer mainPlayer in preferencesOfRole.Keys )
			{
				preferencesOfRole[mainPlayer] = 1;
			}
		}

		//Random picker
		Random random = new();
		float res = (float)random.NextDouble() * total;
		//Search the player with the role
		total = 0;
		foreach ( (SslPlayer player, float preference) in preferencesOfRole )
		{
			total += preference;
			if ( res < total )
			{
				player.RoleHandler.AssignRole(constraint.Role);
				return true;
			}
		}

		return false;
	}
}
