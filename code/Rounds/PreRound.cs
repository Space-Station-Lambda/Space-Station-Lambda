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

        protected override void OnTimeUp()
        {
            AssignRoles();
            base.OnTimeUp();
        }

        private void AssignRoles()
        {
            foreach (MainPlayer mainPlayer in Players)
            {
                mainPlayer.RoleHandler.AssignRandomRole();
                Log.Info($"Assign role {mainPlayer.RoleHandler.Role} to {mainPlayer}");
            }
        }
    }
}