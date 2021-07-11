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
        [Net]
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