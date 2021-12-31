using Sandbox;
using ssl.Modules.Props;
using ssl.Player;
using Prop = ssl.Modules.Props.Instances.Prop;

namespace ssl.Modules.Commands;

public class PropsCommands
{
	/// <summary>
	///     Create props with its id
	/// </summary>
	/// <param name="id">The id of the prop</param>
	[AdminCmd("prop")]
    public static void SpawnProp(string id)
    {
        Client client = ConsoleSystem.Caller;
        SslPlayer sslPlayer = (SslPlayer) client.Pawn;
        PropFactory propFactory = PropFactory.Instance;
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