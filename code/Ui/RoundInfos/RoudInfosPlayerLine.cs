using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;

namespace ssl.Ui.RoundInfos;

public class RoundInfosPlayerLine : Panel
{
    public readonly SslPlayer SslPlayer;

    public RoundInfosPlayerLine(SslPlayer sslPlayer)
    {
        SslPlayer = sslPlayer;
        Log.Info("Création d'un player");
        StyleSheet.Load("Ui/RoundInfos/RoudInfosPlayerLine.scss");
        Add.Label(sslPlayer.Client.Name);
    }
}