using System.Collections.Generic;
using ssl.Player;

namespace ssl.Quests
{
    /// <summary>
    /// Manage quests for a player
    /// </summary>
    public class QuestHandler
    {
        public MainPlayer Player;
        public List<Quest> Quests = new();

        public QuestHandler(MainPlayer player)
        {
            Player = player;
        }
    }
}