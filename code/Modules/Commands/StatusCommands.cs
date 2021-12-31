using Sandbox;
using ssl.Constants;
using ssl.Modules.Statuses;
using ssl.Player;

namespace ssl.Modules.Commands;

public class StatusCommands
{
    [AdminCmd("st_sickness")]
    public static void ApplySickness()
    {
        Client sender = ConsoleSystem.Caller;
        SslPlayer sslPlayer = (SslPlayer) sender.Pawn;
        sslPlayer.StatusHandler.ApplyStatus(StatusFactory.Instance.Create(Identifiers.SICKNESS_ID));
    }
}