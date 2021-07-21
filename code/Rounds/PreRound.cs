using Sandbox;
using ssl.Player;

namespace ssl.Rounds
{
    public class PreRound : BaseRound
    {
        public override string RoundName => "Preround";
        public override int RoundDuration => 5;

        public override BaseRound Next()
        {
            return new InProgressRound();
        }

        public override void OnPlayerSpawn(MainPlayer player)
        {
            base.OnPlayerSpawn(player);
            AddPlayer(player);
        }

        protected override void OnTimeUp()
        {
            AssignRoles();
            base.OnTimeUp();
        }

        private void AssignRoles()
        {
            foreach (MainPlayer mainPlayer in Players)
            {
                Log.Info("Assign role to " + mainPlayer.ToString());
                mainPlayer.RoleHandler.AssignRandomRole();
            }
        }
    }
}
