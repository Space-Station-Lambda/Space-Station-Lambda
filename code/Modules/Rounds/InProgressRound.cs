using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Rounds
{
    public class InProgressRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 600;

        public override BaseRound Next()
        {
            return new ResultsRound("Lambda team");
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

        public override void OnPlayerKilled(MainPlayer player)
        {
            base.OnPlayerKilled(player);
            if (IsRoundFinished())
            {
                Finish();
            }
        }

        private bool IsRoundFinished()
        {
            int numberOfTraitors = 0;
            int numberOfProtagonists = 0;
            foreach (MainPlayer mainPlayer in Players)
            {
                if (mainPlayer.RoleHandler.Role.Id == "traitor") numberOfTraitors++;
                else numberOfProtagonists++;
            }

            Log.Info($"Traitors: {numberOfTraitors}");
            Log.Info($"Protagonists: {numberOfProtagonists}");
            return numberOfTraitors == 0 || numberOfProtagonists == 0;
        }
    }
}