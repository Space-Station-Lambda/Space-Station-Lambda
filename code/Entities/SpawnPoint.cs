using System.Collections.Generic;
using Hammer;
using Sandbox;
using ssl.Player;
using ssl.Player.Roles;

namespace ssl.Entities
{
    [Library("ssl_player_spawn")]
    [EditorModel("models/citizen/citizen.vmdl")]
    [EntityTool("SSL Player Spawnpoint",
        "Space Station Lambda",
        "Defines a point where the player can spawn. Roles can be specified if a spawnpoint is role exclusive")]
    public partial class SpawnPoint : Entity
    {
        [Property(Title = "Role filter")] public string RoleFilter { get; set; } = "";

        /// <summary>
        /// Checks if a player with a specific role can spawn on this SpawnPoint
        /// </summary>
        public bool CanPlayerSpawn(MainPlayer player)
        {
            return player.Role.Id == RoleFilter || RoleFilter == "";
        }
    }
}