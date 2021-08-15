using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;

namespace ssl.UI
{
    public class RoundInfosPlayerLine : Panel
    {
        public Label Name;
        public MainPlayer Player;

        //public Label RoleName;
        public RoundInfosPlayerLine(MainPlayer player)
        {
            StyleSheet.Load("ui/RoudInfosPlayerLine.scss");
            Player = player;
            Name = Add.Label(player.GetClientOwner().Name);
            //RoleName = Add.Label(player.RoleHandler?.Role?.Name);
        }

        public void Update()
        {
            //RoleName.Text = Player.RoleHandler?.Role?.Name;
        }
    }
}