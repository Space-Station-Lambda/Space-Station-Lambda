using System;
using System.Threading.Tasks;
using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Rounds;
using ssl.Player;
using ssl.UI;

namespace ssl
{
    /// <summary>
    /// Gamemode is the base class for SSL.
    /// </summary>
    [Library("ssl")]
    public partial class Gamemode : Game
    {
        /// <summary>
        /// Create a gamemode
        /// </summary>
        public Gamemode()
        {
            Instance = this; //Singleton DP
            if (IsServer) StartServer();
            else if (IsClient) StartClient();
        }

        public static Gamemode Instance { get; private set; }

        /// <summary>
        /// Items in the gamemode
        /// </summary>
        public ItemRegistry ItemRegistry { get; private set; }

        [Net] public Hud Hud { get; set; }
        [Net] public RoundManager RoundManager { get; set; }

        public event Action<MainPlayer> PlayerAddedEvent;
        public event Action<MainPlayer> PlayerRemovedEvent;

        /// <summary>
        /// A client has joined the server. Make them a pawn to play with
        /// </summary>
        public override void ClientJoined(Client client)
        {
            base.ClientJoined(client);
            SpawnPlayer(client);
        }

        /// <summary>
        /// Start server and init classes.
        /// </summary>
        private void StartServer()
        {
            Log.Info("Launching ssl Server...");
            Log.Info("Create Round Manager...");
            RoundManager = new RoundManager();
            Log.Info("Create HUD...");
            ItemRegistry = new ItemRegistry();
            Hud = new Hud();
        }

        /// <summary>
        /// Stat client and init classes
        /// </summary>
        private void StartClient()
        {
            Log.Info("Launching ssl Client...");
            ItemRegistry = new ItemRegistry();
        }

        /// <summary>
        /// Spawn the client and create the player class.
        /// </summary>
        /// <param name="client"></param>
        private void SpawnPlayer(Client client)
        {
            //Init the player.
            MainPlayer player = new();
            client.Pawn = player;
            EmitEvent(player);
            RoundManager.CurrentRound.OnPlayerSpawn(player);
        }

        /// <summary>
        /// Called after the level is loaded
        /// </summary>
        [ClientRpc]
        private void EmitEvent(MainPlayer player)
        {
            PlayerAddedEvent?.Invoke(player);
        }

        public override void PostLevelLoaded()
        {
            _ = StartTickTimer();
            _ = StartSecondTimer();
        }

        /// <summary>
        /// Loop trigger OnSecond() each seconds.
        /// </summary>
        public async Task StartSecondTimer()
        {
            while (true)
            {
                await Task.DelaySeconds(1);
                OnSecond();
            }
        }

        /// <summary>
        /// Loop trigger OnTick() each tick.
        /// </summary>
        public async Task StartTickTimer()
        {
            while (true)
            {
                await Task.NextPhysicsFrame();
                OnTick();
            }
        }

        /// <summary>
        /// Called each seconds
        /// </summary>
        private void OnSecond()
        {
            if (IsServer) RoundManager.CurrentRound?.OnSecond();
        }

        /// <summary>
        /// Called each ticks
        /// </summary>
        private void OnTick()
        {
            if (IsServer) RoundManager.CurrentRound?.OnTick();
        }
    }
}