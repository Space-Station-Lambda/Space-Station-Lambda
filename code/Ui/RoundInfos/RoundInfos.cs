using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.Ui.RoundInfos
{
    public class RoundInfos : Panel
    {
        private readonly List<RoundInfosPlayerLine> roundPlayers = new();

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
            if (scorePressed) UpdatePlayers();
        }

        public void OnPlayerAdded(MainPlayer player)
        {
            AddPlayer(player);
        }

        /// <summary>
        /// Add player to the round info
        /// </summary>
        /// <param name="player">The player to be added</param>
        private void AddPlayer(MainPlayer player)
        {
            RoundInfosPlayerLine roudInfosPlayerLine = new(player);
            roundPlayers.Add(roudInfosPlayerLine);
            AddChild(roudInfosPlayerLine);
        }

        /// <summary>
        /// Remove player from round info
        /// </summary>
        /// <param name="player">The player to be removed</param>
        private void RemovePlayer(MainPlayer player)
        {
            foreach (RoundInfosPlayerLine roundPlayer in roundPlayers.Where(roundPlayer => roundPlayer.Player.Equals(player)))
            {
                //Delete the element
                roundPlayer.Delete();
                //Remove from the list
                roundPlayers.Remove(roundPlayer);
            }
        }

        private void UpdatePlayers()
        {
            foreach (RoundInfosPlayerLine roundPlayer in roundPlayers)
            {
                roundPlayer.Update();
            }
        }
    }
}