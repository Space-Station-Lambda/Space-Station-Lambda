using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;

namespace ssl.Ui.RoundInfos
{
    public class RoundInfosPlayerLine : Panel
    {
        public readonly MainPlayer Player;

        public RoundInfosPlayerLine(MainPlayer player)
        {
            Player = player;
            Log.Info("Création d'un player");
            StyleSheet.Load("Ui/RoundInfos/RoudInfosPlayerLine.scss");
            Add.Label(player.GetClientOwner().Name);
        }
    }
}