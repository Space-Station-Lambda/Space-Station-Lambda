using System.Collections.Generic;
using System.Linq;
using Sandbox.UI;
using ssl.Quests;

namespace ssl.UI
{
    public class QuestPane : Panel
    {
        private Dictionary<Quest, Label> quests = new();

        public QuestPane(List<Quest> quests)
        {
            quests.ForEach(q => this.quests.Add(q, new Label()));
        }

        private void UpdateLabels()
        {
            foreach (Quest quest in quests.Keys)
            {
                quests[quest].Text = quest.Name;
            }
        }
        
        public override void Tick()
        {
            UpdateLabels();
        }
    }
}