using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.Ui.RoundInfos
{
    public class RoundInfos : Panel
    {
        private readonly List<RoundInfosPlayerLine> roundInfosPlayerLines = new();

        public RoundInfos()
        {
            StyleSheet.Load("Ui/RoundInfos/RoundInfos.scss");
            Log.Info("Register event...");
        }

        public override void Tick()
        {
            base.Tick();
            bool scorePressed = Input.Down(InputButton.Score);
            SetClass("hidden", !scorePressed);
            if (scorePressed) UpdatePlayers();
        }


        /// <summary>
        /// Add player to the round info
        /// </summary>
        /// <param name="player">The player to be added</param>
        private void AddPlayer(Player.Player player)
        {
            RoundInfosPlayerLine roudInfosPlayerLine = new(player);
            AddChild(roudInfosPlayerLine);
            roundInfosPlayerLines.Add(roudInfosPlayerLine);
        }


        /// <summary>
        /// Delete round info
        /// </summary>
        /// <param name="roundInfosPlayerLine">Round info to delete</param>
        private void RemoveRoundInfosPlayerLine(RoundInfosPlayerLine roundInfosPlayerLine)
        {
            roundInfosPlayerLine.Delete();
            roundInfosPlayerLines.Remove(roundInfosPlayerLine);
        }

        private void UpdatePlayers2()
        {
        }

        private List<Player.Player> PlayersToAdd()
        {
            List<Player.Player> players = new();
            foreach (Client client in Client.All)
            {
                Player.Player player = (Player.Player)client.Pawn;
                bool founded = false;
                foreach (RoundInfosPlayerLine roundInfosPlayerLine in roundInfosPlayerLines)
                {
                    if (roundInfosPlayerLine.Player.Equals(player)) founded = true;
                }

                if (!founded) players.Add(player);
            }

            return players;
        }

        private List<RoundInfosPlayerLine> RoundInfosPlayerLinesToRemove()
        {
            List<RoundInfosPlayerLine> _roundInfosPlayerLines = new();
            foreach (RoundInfosPlayerLine roundInfosPlayerLine in roundInfosPlayerLines)
            {
                Player.Player player = roundInfosPlayerLine.Player;
                bool founded = false;
                foreach (Client client in Client.All)
                {
                    if (((Player.Player)client.Pawn).Equals(player)) founded = true;
                    break;
                }

                if (!founded) _roundInfosPlayerLines.Add(roundInfosPlayerLine);
            }

            return _roundInfosPlayerLines;
        }

        private void UpdatePlayers()
        {
            List<Player.Player> playersToAdd = PlayersToAdd();
            List<RoundInfosPlayerLine> roundInfosPlayerLinesToRemove = RoundInfosPlayerLinesToRemove();
            foreach (Player.Player mainPlayer in playersToAdd)
            {
                AddPlayer(mainPlayer);
            }

            foreach (RoundInfosPlayerLine roundInfosPlayerLine in roundInfosPlayerLinesToRemove)
            {
                RemoveRoundInfosPlayerLine(roundInfosPlayerLine);
            }
        }
    }
}