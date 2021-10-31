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
            MainPlayer player = (MainPlayer)client.Pawn;
            PropFactory propFactory = new();
            try
            {
                Prop prop = propFactory.Create(id);
                prop.Position = player.EyePos + player.EyeRot.Forward * 50;
                prop.Rotation = player.EyeRot;
            }
            catch
            {
                Log.Info($"{id} not found.");
            }
        }
    }
}