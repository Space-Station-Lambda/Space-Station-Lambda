using System;
using Sandbox;
using SSL.Player;
using SSL.UI;

namespace SSL
{
    [Library("SSL")]
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
            Log.Info("Launching SSL Server...");
            // Create a HUD entity. This entity is globally networked
            hud = new Hud();
        }

        private void StartClient()
        {
            if (IsServer) throw new Exception("Invalid Context");
            Log.Info("Launching SSL Client...");
        }

        private void SpawnPlayer(Client client)
        {
            MainPlayer player = new();
            client.Pawn = player;
            player.Respawn();
        }
    }
}