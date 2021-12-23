using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Items.Instances;
using ssl.Modules.Skills;
using ssl.Player;

namespace ssl.Modules.Commands;

public class SkillsCommands
{
	/// <summary>
	///     Clear the inventory
	/// </summary>
	[AdminCmd("scheck")]
	public static void SkillCheck(string id, int complexity = 50)
	{
		Client client = ConsoleSystem.Caller;
		SslPlayer sslPlayer = (SslPlayer)client.Pawn;
		bool result = sslPlayer.SkillHandler.TrySkillCheck(id, complexity);
	}
	
	/// <summary>
	///     Clear the inventory
	/// </summary>
	[AdminCmd("scu")]
	public static void SkillCheck()
	{
		Client client = ConsoleSystem.Caller;
		SslPlayer sslPlayer = (SslPlayer)client.Pawn;
		foreach ((string key, Skill value) in sslPlayer.SkillHandler.GetSkills())
		{
			Log.Info($"[Skill]{key}:{value.Level}");
		}
	}
	
}
