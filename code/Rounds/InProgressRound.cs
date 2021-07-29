using System.Collections.Generic;
using Sandbox;
using ssl.Player;
using SpawnPoint = ssl.Entities.SpawnPoint;

namespace ssl.Rounds
{
    public class InProgressRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 600;

        public override BaseRound Next()
        {
            return new PreRound();
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (Host.IsServer)
            {
                List<SpawnPoint> spawnPoints = SpawnPoint.GetAllSpawnPoints();
                
                foreach (Client client in Client.All)
                {
                    if (client.Pawn is MainPlayer player)
                    {
                        if (spawnPoints.Count > 0)
                        {
                            foreach (SpawnPoint point in spawnPoints)
                            {
                                if (point.CanRoleSpawn(player.RoleHandler.Role))
                                {
                                    player.Respawn(point);
                                }
                            
                                break;
                            }
                        }
                        else
                        {
                            player.Respawn();
                        }
                    }
                }
            }
        }

        public override void OnPlayerSpawn(MainPlayer player)
        {
            //TODO Player wait before spawn
            base.OnPlayerSpawn(player);
        }
    }
}