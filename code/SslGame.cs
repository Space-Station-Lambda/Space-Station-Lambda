using System;
using System.Threading.Tasks;
using Sandbox;
using ssl.Player;
using ssl.Rounds;
using ssl.UI;

namespace ssl
{
    [Library("ssl")]
    public class SslGame : Game
    {
        public static SslGame Instance { get; private set; }
        [Net]
        public RoundManager RoundManager { get; private set; } = new();
        public SslGame()
        {
            Instance = this;
            RoundManager = new RoundManager();
            if (IsServer) StartServer();
            if (IsClient) StartClient();
        }

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
            Log.Info("Start Round Manager...");
            Log.Info("Create HUD...");
            _ = new Hud();
        }

        private void StartClient()
        {
            if (IsServer) throw new Exception("Invalid Context");
            Log.Info("Launching ssl Client...");
        }

        private void SpawnPlayer(Client client)
        {
            MainPlayer player = new();
            client.Pawn = player;
            player.Respawn();
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
                await Task.DelaySeconds( 1 );
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