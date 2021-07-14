using Sandbox;
using ssl.Player;

namespace ssl.Rounds
{
    public class InProgressRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 100;
        
        public override BaseRound Next()
        {
            return new PreRound();
        }

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

        public override void OnPlayerSpawn(MainPlayer player)
        {
            //TODO Player wait before spawn
            base.OnPlayerSpawn(player);
        }
    }
}