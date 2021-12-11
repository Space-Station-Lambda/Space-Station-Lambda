using System.Collections.Generic;
using System.Linq;
using Hammer;
using Sandbox;
using ssl.Modules.Roles;
using ssl.Modules.Roles.Instances;

namespace ssl.Modules.Rounds;

[Library("ssl_player_spawn")]
[EditorModel("models/citizen/citizen.vmdl")]
[EntityTool("SSL Player Spawnpoint",
	"Space Station Lambda",
	"Defines a point where the player can spawn. Roles can be specified if a spawnpoint is role exclusive")]
public class SpawnPoint : Entity
{
	public const string SPAWN_POINT_TAG = "spawnpoint";

	[Property(Title = "Role filter")] public string RoleFilter { get; set; } = "";

	public override void OnActive()
	{
		base.OnActive();
		Tags.Add(SPAWN_POINT_TAG);
	}

	/// <summary>
	///     Checks if a player with a specific role can spawn on this SpawnPoint
	/// </summary>
	public bool CanRoleSpawn( Role role )
	{
		return role.Id == RoleFilter || RoleFilter == "";
	}

	public static List<SpawnPoint> GetAllSpawnPoints()
	{
		return (from entity in All where entity.Tags.Has(SPAWN_POINT_TAG) select entity as SpawnPoint).ToList();
	}
}
