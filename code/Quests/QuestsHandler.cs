using System.Collections.Generic;
using ssl.Player;

namespace ssl.Quests
{
    /// <summary>
    /// Manage quests for a player
    /// </summary>
    public class QuestsHandler
    {
        public MainPlayer Player;
        public List<Quest> Quests = new();

        public QuestsHandler(MainPlayer player)
        {
            Player = player;
        }
    }
}