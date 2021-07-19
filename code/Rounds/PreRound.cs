using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Rounds
{
    public class PreRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 10;

        protected override void OnStart()
        {
            base.OnStart();
            List<Entities.SpawnPoint> spawnPoints = GetSpawnPoints();
            if (Host.IsServer)
            {
                foreach (Client client in Client.All)
                {
                    if (client.Pawn is MainPlayer player)
                    {
                        foreach (Entities.SpawnPoint point in spawnPoints)
                        {
                            if (point.CanRoleSpawn(player.Role))
                            {
                                player.Respawn(point);
                            }
                            
                            break;
                        }
                    }
                }
            }
        }

        public override BaseRound Next()
        {
            return new InProgressRound();
        }

        public override void OnPlayerSpawn(MainPlayer player)
        {
            base.OnPlayerSpawn(player);
            AddPlayer(player);
        }

        private List<Entities.SpawnPoint> GetSpawnPoints()
        {
            List<Entities.SpawnPoint> spawnPoints = new();
            
            foreach (Entity entity in Entity.All)
            {
                if (entity.Tags.Has("spawnpoint"))
                {
                    spawnPoints.Add(entity as Entities.SpawnPoint);
                }
            }

            return spawnPoints;
        }
    }
}