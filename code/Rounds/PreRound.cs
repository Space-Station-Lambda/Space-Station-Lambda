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
            if (Host.IsServer)
            {
                foreach (Client client in Client.All)
                {
                    if (client.Pawn is MainPlayer player)
                    {
                        player.Respawn();
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
    }
}