using Sandbox;
using ssl.Player;

namespace ssl.Modules.Commands
{
    public class TestCommands
    {
        [AdminCmd("save")]
        public static void Save(string id)
        {
            Client client = ConsoleSystem.Caller;
            MainPlayer player = (MainPlayer)client.Pawn;
        }
    }
}