using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Factories;
using ssl.Modules.Items;
using ssl.Modules.Roles.Types.Antagonists;
using ssl.Modules.Roles.Types.Jobs;
using ssl.Modules.Roles.Types.Others;
using ssl.Modules.Skills;
using ssl.Player;

namespace ssl.Modules.Roles;

/// <summary>
///     Player's role
/// </summary>
public abstract class Role : BaseNetworkable
{
	public static Dictionary<string, Role> All = new()
	{
		{"assistant", new Assistant()},
		{"captain", new Captain()},
		{"engineer", new Engineer()},
		{"ghost", new Ghost()},
		{"guard", new Guard()},
		{"janitor", new Janitor()},
		{"scientist", new Scientist()},
		{"traitor", new Traitor()}
	};

	public abstract string Id { get; }
	public abstract string Name { get; }
	public abstract string Description { get; }
	public virtual string Category => "";
	public virtual string Model => "models/units/simpleterry.vmdl";
	public virtual IEnumerable<string> Clothing => new HashSet<string>();
	public virtual IEnumerable<string> Items => new List<string>();

	/// <summary>
	///     The skillset of the Role. For now the role handle skills but we can change that later for let the player
	///     handle his roles
	/// </summary>
	public virtual SkillSet SkillSet => new();

	/// <summary>
	///     Array of factions of the role. A role can have multiple factions.
	/// </summary>
	public virtual Faction[] Faction => new[] {Roles.Faction.Protagonists};

	/// <summary>
	///     Trigger when the role is assigned
	/// </summary>
	public virtual void OnAssigned( SslPlayer sslPlayer )
	{
	}

	/// <summary>
	///     Trigger when the player spawn
	/// </summary>
	/// <param name="sslPlayer"></param>
	public virtual void OnSpawn( SslPlayer sslPlayer )
	{
		foreach ( string itemId in Items )
		{
			ItemFactory itemFactory = ItemFactory.Instance;
			try
			{
				sslPlayer.Inventory.Add(itemFactory.Create(itemId));
			}
			catch ( ArgumentException e )
			{
				Log.Error($"{itemId} can't be created because of: {e.Message}");
			}
		}

		sslPlayer.ClothesHandler.AttachClothes(Clothing);
	}

	/// <summary>
	///     Trigger when the role is unassigned
	/// </summary>
	public virtual void OnUnassigned( SslPlayer sslPlayer )
	{
	}

	/// <summary>
	///     Trigger when a player with the role is killed
	/// </summary>
	public virtual void OnKilled( SslPlayer sslPlayer )
	{
		sslPlayer.RoleHandler.AssignRole(new Ghost());
		sslPlayer.Respawn(sslPlayer.Position, sslPlayer.Rotation);
	}

	public override string ToString()
	{
		return $"[{Id}]{Name}";
	}

	protected bool Equals( Role other )
	{
		return Id == other.Id;
	}

	public override bool Equals( object obj )
	{
		if ( ReferenceEquals(null, obj) )
		{
			return false;
		}

		if ( ReferenceEquals(this, obj) )
		{
			return true;
		}

		if ( obj.GetType() != GetType() )
		{
			return false;
		}

		return Equals((Role)obj);
	}

	public override int GetHashCode()
	{
		return Id != null ? Id.GetHashCode() : 0;
	}
}
