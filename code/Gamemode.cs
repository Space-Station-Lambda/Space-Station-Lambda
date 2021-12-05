using System;
using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Props;
using ssl.Modules.Rounds;
using ssl.Player;
using ssl.Ui;

namespace ssl;

/// <summary>
///     Gamemode is the base class for SSL.
/// </summary>
public partial class Gamemode : Game
{
	/// <summary>
	///     Create a gamemode
	/// </summary>
	public Gamemode()
	{
		Instance = this; //Singleton DP
		if ( IsServer )
		{
			StartServer();
		}
		else if ( IsClient )
		{
			StartClient();
		}
	}

	public static Gamemode Instance { get; private set; }

	[Net] public RoundManager RoundManager { get; private set; }

	/// <summary>
	///     A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( Client client )
	{
		base.ClientJoined(client);
		SpawnPlayer(client);
	}

	/// <summary>
	///     Start server and init classes.
	/// </summary>
	private void StartServer()
	{
		Log.Info("Launching ssl Server...");
		Log.Info("Create Round Manager...");
		RoundManager = new RoundManager();
		Log.Info("Create HUD...");
		_ = new Hud();
		LoadDatabase();
	}

	private void LoadDatabase()
	{
		Log.Info("Load database...");
		// Factories create the dao when they appear.
		// Maybe the databse create have to be outside dao; i don't know :(
		_ = ItemFactory.Instance;
		_ = PropFactory.Instance;
	}
	/// <summary>
	///     Stat client and init classes
	/// </summary>
	private void StartClient()
	{
		Log.Info("Launching ssl Client...");
	}

	/// <summary>
	///     Spawn the client and create the player class.
	/// </summary>
	/// <param name="client"></param>
	private void SpawnPlayer( Client client )
	{
		//Init the player.
		SslPlayer sslPlayer = new();
		client.Pawn = sslPlayer;
		RoundManager.CurrentRound.OnPlayerSpawn(sslPlayer);
	}

	public override void PostLevelLoaded()
	{
		StartTickTimer();
		StartSecondTimer();
	}

	public override void OnVoicePlayed( long playerId, float level )
	{
		//TODO: Add voice volume to player
	}

	/// <summary>
	///     Loop trigger OnSecond() each seconds.
	/// </summary>
	private async void StartSecondTimer()
	{
		while ( true )
		{
			try
			{
				await Task.DelaySeconds(1);
				OnSecond();
			}
			catch ( Exception e )
			{
				// If we close the server we don't care about the Task
				Log.Error(e);
			}
		}
	}

	/// <summary>
	///     Loop trigger OnTick() each tick.
	/// </summary>
	private async void StartTickTimer()
	{
		while ( true )
		{
			try
			{
				await Task.NextPhysicsFrame();
				OnTick();
			}
			catch ( Exception e )
			{
				// If we close the server we don't care about the Task
				Log.Error(e);
			}
		}
	}

	/// <summary>
	///     Called each seconds
	/// </summary>
	private void OnSecond()
	{
		if ( IsServer )
		{
			RoundManager.CurrentRound?.OnSecond();
		}
	}

	/// <summary>
	///     Called each ticks
	/// </summary>
	private void OnTick()
	{
		if ( IsServer )
		{
			RoundManager.CurrentRound?.OnTick();
		}
	}
}
