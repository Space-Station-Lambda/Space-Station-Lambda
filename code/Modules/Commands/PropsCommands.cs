using Sandbox;
using ssl.Modules.Elements.Props;
using ssl.Player;
using Prop = ssl.Modules.Elements.Props.Types.Prop;

namespace ssl.Modules.Commands
{
    public class PropsCommands
    {
        /// <summary>
        /// Create props with its id
        /// </summary>
        /// <param name="id">The id of the prop</param>
        [AdminCmd("prop")]
        public static void SpawnProp(string id)
        {
            Client client = ConsoleSystem.Caller;
            Player.SslPlayer sslPlayer = (Player.SslPlayer)client.Pawn;
            PropFactory propFactory = new();
            try
            {
                Prop prop = propFactory.Create(id);
                prop.Position = sslPlayer.EyePos + sslPlayer.EyeRot.Forward * 50;
                prop.Rotation = sslPlayer.EyeRot;
            }
            catch
            {
                Log.Info($"{id} not found.");
            }
        }
    }
}