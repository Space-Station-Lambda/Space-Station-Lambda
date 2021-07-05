using System;
using Sandbox;
using ssl.Player;
using ssl.UI;

namespace ssl
{
    [Library("ssl")]
    public partial class SslGame : Game
    {
        private Hud hud;

        public SslGame()
        {
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
            hud = new Hud();
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