using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Player;
using Steamworks;

namespace ssl.UI
{
    public class RoundInfos : Panel
    {

        private readonly List<RoundPlayer> roundPlayers = new();
        
        public RoundInfos()
        {
            StyleSheet.Load("ui/roundinfos.scss");
            Log.Info("Register event...");
            Gamemode.Instance.PlayerAddedEvent += OnPlayerAdded;

        }

        public override void Tick()
        {
            base.Tick();
            bool scorePressed = Input.Down(InputButton.Score);
            SetClass("hidden", !scorePressed);
            if(scorePressed) UpdatePlayers();
        }

        public void OnPlayerAdded(object sender, PlayerAddedEventArgs e)
        {
            AddPlayer(e.Player);
        }
        
        /// <summary>
        /// Add player to the round info
        /// </summary>
        /// <param name="player">The player to be added</param>
        private void AddPlayer(MainPlayer player)
        {
            RoundPlayer roundPlayer = new(player);
            roundPlayers.Add(roundPlayer);
            AddChild(roundPlayer);
        }
        
        /// <summary>
        /// Remove player from round info
        /// </summary>
        /// <param name="player">The player to be removed</param>
        private void RemovePlayer(MainPlayer player)
        {
            foreach (RoundPlayer roundPlayer in roundPlayers.Where(roundPlayer => roundPlayer.Player.Equals(player)))
            {
                //Delete the element
                roundPlayer.Delete();
                //Remove from the list
                roundPlayers.Remove(roundPlayer);
            }
        }

        private void UpdatePlayers()
        {
            foreach (RoundPlayer roundPlayer in roundPlayers)
            {
                roundPlayer.Update();
            }
        }

        public class RoundPlayer : Panel
        {
            public MainPlayer Player;
            public Label Name;
            //public Label RoleName;
            public RoundPlayer(MainPlayer player)
            {
                Player = player;
                Name = Add.Label(player.GetClientOwner().Name);
                //RoleName = Add.Label(player.RoleHandler?.Role?.Name);
            }

            public void Update()
            {
                //RoleName.Text = Player.RoleHandler?.Role?.Name;
            }
        }
    }
}
