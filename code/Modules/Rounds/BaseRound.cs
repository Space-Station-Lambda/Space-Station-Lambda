using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Constants;
using ssl.Modules.Milestones;
using ssl.Modules.Roles;
using ssl.Modules.Scenarios;
using ssl.Player;

namespace ssl.Modules.Rounds;

public abstract partial class BaseRound : BaseNetworkable
{
	protected BaseRound()
	{
		Scenario scenario = ScenarioFactory.Instance.Create($"{Identifiers.Role}{Identifiers.Separator}{Identifiers.Basic}");
		RoleDistributor = new RoleDistributor(scenario, Players);
		MilestonesHandler = new MilestonesHandler();
	}

	[Net] public List<SslPlayer> Players { get; set; } = new();
	public virtual int RoundDuration => 0;
	public virtual string RoundName => "";
	public float RoundEndTime { get; set; }
	public float TimeLeft => RoundEndTime - Time.Now;
	public MilestonesHandler MilestonesHandler { get; }
	public RoleDistributor RoleDistributor { get; }
	public event Action<BaseRound> RoundEndedEvent;

	public void Start()
	{
		if ( Host.IsServer && RoundDuration > 0 )
		{
			RoundEndTime = Time.Now + RoundDuration;
			InitPlayers();
		}

		OnStart();
	}

	public void Stop()
	{
		Log.Info($"[Round] Round {this} stopped");
		if ( Host.IsServer )
		{
			RoundEndTime = 0f;
			Players.Clear();
		}
	}

	/// <summary>
	///     When the round is finish
	/// </summary>
	public void Finish()
	{
		Log.Info($"[Round] Round {this} finished");
		OnFinish();
		RoundEndedEvent?.Invoke(this);
	}

	public void AddPlayer( SslPlayer sslPlayer )
	{
		Log.Info($"[Round] Add player {sslPlayer} to the round");
		Host.AssertServer();
		Players.Add(sslPlayer);
	}

	public void RemovePlayer( SslPlayer sslPlayer )
	{
		Log.Info($"[Round] Remove player {sslPlayer} to the round");
		Host.AssertServer();
		Players.Remove(sslPlayer);
	}

	public abstract BaseRound Next();

	public virtual void OnPlayerSpawn( SslPlayer sslPlayer )
	{
		AddPlayer(sslPlayer);
	}

	public virtual void OnPlayerKilled( SslPlayer sslPlayer )
	{
		RemovePlayer(sslPlayer);
	}

	public virtual void OnPlayerLeave( SslPlayer sslPlayer )
	{
	}

	public virtual void OnTick()
	{
	}

	public virtual void OnSecond()
	{
		if ( Host.IsServer )
		{
			if ( RoundEndTime > 0 && Time.Now >= RoundEndTime )
			{
				RoundEndTime = 0f;
				OnTimeUp();
			}
		}
	}

	protected virtual void OnStart()
	{
	}

	/// <summary>
	///     Default event when the round is finished.
	/// </summary>
	protected virtual void OnFinish()
	{
	}

	/// <summary>
	///     Default event when times is up.
	/// </summary>
	protected virtual void OnTimeUp()
	{
		Finish();
	}

	/// <summary>
	///     Set players to the list
	/// </summary>
	private void InitPlayers()
	{
		foreach ( Client client in Client.All )
		{
			Players.Add((SslPlayer)client.Pawn);
		}
	}

	public override string ToString()
	{
		return $"Round Name: {RoundName}\n" +
		       $"Round Duration: {RoundDuration}\n";
	}
}
