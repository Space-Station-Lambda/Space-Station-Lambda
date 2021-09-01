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
            Client sender = Sandbox.ConsoleSystem.Caller;
            MainPlayer player = (MainPlayer)sender.Pawn;
            player.StatusHandler.ApplyStatus(new Sickness(6));
        }
    }
}