using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;

namespace ssl.Ui.RoundInfos
{
    public class RoundInfosPlayerLine : Panel
    {
        public readonly Player.SslPlayer SslPlayer;

        public RoundInfosPlayerLine(Player.SslPlayer sslPlayer)
        {
            SslPlayer = sslPlayer;
            Log.Info("Création d'un player");
            StyleSheet.Load("Ui/RoundInfos/RoudInfosPlayerLine.scss");
            Add.Label(sslPlayer.GetClientOwner().Name);
        }
    }
}