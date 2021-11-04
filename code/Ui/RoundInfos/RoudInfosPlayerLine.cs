using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;

namespace ssl.Ui.RoundInfos
{
    public class RoundInfosPlayerLine : Panel
    {
        public readonly Player.Player Player;

        public RoundInfosPlayerLine(Player.Player player)
        {
            Player = player;
            Log.Info("Création d'un player");
            StyleSheet.Load("Ui/RoundInfos/RoudInfosPlayerLine.scss");
            Add.Label(player.GetClientOwner().Name);
        }
    }
}