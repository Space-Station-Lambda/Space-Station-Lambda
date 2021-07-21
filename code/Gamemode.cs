using System;
using System.Threading.Tasks;
using Sandbox;
using ssl.Items.Data;
using ssl.Player;
using ssl.Rounds;
using ssl.UI;

namespace ssl
{
    [Library("ssl")]
    public partial class Gamemode : Game
    {
        public Gamemode()
        {
            Instance = this;
            if (IsServer) StartServer();
            if (IsClient) StartClient();
        }

        public static Gamemode Instance { get; private set; }

        public ItemRegistry ItemRegistry { get; private set; }
        [Net] public Hud Hud { get; set; }
        [Net] public RoundManager RoundManager { get; set; }

        /// <summary>
        /// A client has joined the server. Make them a pawn to play with
        /// </summary>
        public override void ClientJoined(Client client)
        {
            base.ClientJoined(client);
            SpawnPlayer(client);
        }

        private void StartServer()
        {
            if (IsClient) throw new Exception("Invalid Context");
            Log.Info("Launching ssl Server...");
            Log.Info("Create Round Manager...");
            RoundManager = new RoundManager();
            Log.Info("Create HUD...");
            Hud = new Hud();
            ItemRegistry = new ItemRegistry();
        }

        private void StartClient()
        {
            if (IsServer) throw new Exception("Invalid Context");
            Log.Info("Launching ssl Client...");
            ItemRegistry = new ItemRegistry();
        }

        private void SpawnPlayer(Client client)
        {
            MainPlayer player = new();
            client.Pawn = player;
            RoundManager.CurrentRound.OnPlayerSpawn(player);
        }

        public override void PostLevelLoaded()
        {
            _ = StartTickTimer();
            _ = StartSecondTimer();
        }

        public async Task StartSecondTimer()
        {
            while (true)
            {
                await Task.DelaySeconds(1);
                OnSecond();
            }
        }

        public async Task StartTickTimer()
        {
            while (true)
            {
                await Task.NextPhysicsFrame();
                OnTick();
            }
        }

        private void OnSecond()
        {
            if (IsServer)
            {
                RoundManager.CurrentRound?.OnSecond();
            }
        }

        private void OnTick()
        {
            if (IsServer)
            {
                RoundManager.CurrentRound?.OnTick();
            }
        }
    }
}