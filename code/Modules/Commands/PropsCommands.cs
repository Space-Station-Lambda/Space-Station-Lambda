using Sandbox;
using ssl.Modules.Props.Types;
using ssl.Player;
using Prop = ssl.Modules.Props.Prop;

namespace ssl.Modules.Commands
{
    public class PropsCommands
    {
        [AdminCmd("pp_stain")]
        public static void RestartRound()
        {
            Client client = Sandbox.ConsoleSystem.Caller;
            MainPlayer player = (MainPlayer)client.Pawn;
            Prop ent = new Stain
            {
                Position = player.EyePos + player.EyeRot.Forward * 50,
                Rotation = player.EyeRot
            };
        }
    }
}