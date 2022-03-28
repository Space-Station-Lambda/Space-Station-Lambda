using System;
using System.Threading.Tasks;
using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Props;
using ssl.Modules.Roles;
using ssl.Modules.Rounds;
using ssl.Modules.Scenarios;
using ssl.Modules.Skills;
using ssl.Modules.Statuses;
using ssl.Player;
using ssl.Ui;
using WorldEntity = ssl.Modules.WorldEntity;

namespace ssl;

/// <summary>
///     Gamemode is the base class for SSL.
/// </summary>
public partial class Gamemode : Game
{
    public event Action<SslPlayer> PlayerSpawned;
    public event Action PlayerDisconnected;
    public event Action<SslPlayer> PlayerKilled;
    
	/// <summary>
	///     Create a gamemode
	/// </summary>
	public Gamemode()
    {
        Game.Current = this; //Singleton DP
        if (IsServer) StartServer();
        else if (IsClient) StartClient();
    }

    public new static Gamemode Current => (Gamemode) Game.Current;

    [Net] public RoundManager RoundManager { get; private set; }

    public override void PostLevelLoaded()
    {
        RegisterWorldEntities();
        _ = StartSecondTimer();
    }

    /// <summary>
    ///     A client has joined the server. Make them a pawn to play with
    /// </summary>
    public override void ClientJoined(Client client)
    {
        base.ClientJoined(client);
        SpawnPlayer(client);
    }

    public override void ClientDisconnect(Client cl, NetworkDisconnectionReason reason)
    {
        base.ClientDisconnect(cl, reason);
        PlayerDisconnected?.Invoke();
    }

    private void OnPlayerKilled(SslPlayer player)
    {
        PlayerKilled?.Invoke(player);
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
        
        Map.Physics.Gravity = Vector3.Zero;
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
    private void SpawnPlayer(Client client)
    {
        //Init the player.
        SslPlayer sslPlayer = new();
        sslPlayer.PlayerKilled += OnPlayerKilled;
        client.Pawn = sslPlayer;
        PlayerSpawned?.Invoke(sslPlayer);
    }

    private void LoadDatabase()
    {
        Log.Info("Load database...");
        // Factories create the dao when they appear.
        // Maybe the databse create have to be outside dao; i don't know :(
        _ = ItemDao.Instance;
        _ = PropDao.Instance;
        _ = RoleDao.Instance;
        _ = StatusDao.Instance;
        _ = ScenarioDao.Instance;
        _ = SkillDao.Instance;
    }

    private void RegisterWorldEntities()
    {
        Log.Info("Registering all world entities in DAOs...");
        foreach (Entity entity in All)
        {
            if (entity is WorldEntity worldEntity)
            {
                worldEntity.RegisterDao();
            }
        }
    }

    /// <summary>
    ///     Loop trigger OnSecond() each seconds.
    /// </summary>
    private async Task StartSecondTimer()
    {
        while (true)
        {
            await Task.DelaySeconds(1);
            OnSecond();
        }
    }

    /// <summary>
    ///     Called each seconds
    /// </summary>
    private void OnSecond()
    {
        if (IsServer) RoundManager.CurrentRound?.OnSecond();
    }

    /// <summary>
    ///     Called each ticks
    /// </summary>
    [Event.Tick]
    private void OnTick()
    {
        if (IsServer) RoundManager.CurrentRound?.OnTick();
    }
}