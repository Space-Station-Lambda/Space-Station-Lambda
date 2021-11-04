using Sandbox;
using ssl.Modules.Statuses.Types;
using ssl.Player;

namespace ssl.Modules.Commands
{
    public class StatusCommands
    {
        [AdminCmd("st_sickness")]
        public static void ApplySickness()
        {
            Client sender = ConsoleSystem.Caller;
            Player.SslPlayer sslPlayer = (Player.SslPlayer)sender.Pawn;
            sslPlayer.StatusHandler.ApplyStatus(new Sickness(6));
        }
    }
}