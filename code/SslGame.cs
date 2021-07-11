using System;
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
        public BaseRound Round { get; private set; } = new PreRound();

        public SslGame()
        {
            Instance = this;
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
            // Create a HUD entity. This entity is globally networked
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

        
    }
}