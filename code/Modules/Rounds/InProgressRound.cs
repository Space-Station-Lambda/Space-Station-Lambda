using Sandbox;
using ssl.Modules.Roles.Instances;
using ssl.Player;

namespace ssl.Modules.Rounds;

public class InProgressRound : BaseRound
{
	private const string TraitorId = "traitor";

	public override string RoundName => "Preround";
	public override int RoundDuration => 600;

	public override BaseRound Next()
	{
		int numberOfTraitors = 0;
		int numberOfProtagonists = 0;
		foreach ( SslPlayer mainPlayer in Players )
		{
			if ( mainPlayer.RoleHandler.Role.Id == TraitorId )
			{
				numberOfTraitors++;
			}
			else
			{
				numberOfProtagonists++;
			}
		}

		return numberOfTraitors switch
		{
			0 when numberOfProtagonists > 0 => new EndRound(RoundOutcome.ProtagonistsWin),
			> 0 when numberOfProtagonists == 0 => new EndRound(RoundOutcome.TraitorsWin),
			_ => new EndRound(RoundOutcome.Tie)
		};
	}

	protected override void OnStart()
	{
		base.OnStart();

		if ( Host.IsServer )
		{
			var spawnPoints = SpawnPoint.GetAllSpawnPoints();

			foreach ( Client client in Client.All )
			{
				if ( client.Pawn is SslPlayer player )
				{
					if ( spawnPoints.Count > 0 )
					{
						foreach ( SpawnPoint point in spawnPoints )
						{
							if ( point.CanRoleSpawn(player.RoleHandler.Role) )
							{
								player.Respawn(point);
							}

							break;
						}
					}
					else
					{
						player.Respawn();
					}
				}
			}
		}
	}

	public override void OnPlayerKilled( SslPlayer sslPlayer )
	{
		base.OnPlayerKilled(sslPlayer);
		if ( IsRoundFinished() )
		{
			Finish();
		}
	}

	private bool IsRoundFinished()
	{
		int numberOfTraitors = 0;
		int numberOfProtagonists = 0;
		foreach ( SslPlayer mainPlayer in Players )
		{
			if ( mainPlayer.RoleHandler.Role is Traitor )
			{
				numberOfTraitors++;
			}
			else
			{
				numberOfProtagonists++;
			}
		}

		Log.Info($"[InProgressRound] Traitors: {numberOfTraitors}");
		Log.Info($"[InProgressRound] Protagonists: {numberOfProtagonists}");
		return numberOfTraitors == 0 || numberOfProtagonists == 0;
	}
}
