using Sandbox;
using ssl.Player;

namespace ssl.Rounds
{
    public class PreRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 10;

        public override BaseRound Next()
        {
            return new InProgressRound();
        }

        public override void OnPlayerSpawn(MainPlayer player)
        {
            base.OnPlayerSpawn(player);
            AddPlayer(player);
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            AssignRoles();
        }

        private void AssignRoles()
        {
            foreach (MainPlayer mainPlayer in Players)
            {
                mainPlayer.RoleHandler.AssignRandomRole();
            }
        }
    }
}