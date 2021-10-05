using System.Collections.Generic;

namespace ssl.Modules.Milestones
{
    public class MilestonesHandler
    {
        /// <summary>
        /// Dictionary of milestones; bool represents if the milestone is completed
        /// </summary>
        public List<MilestoneCompletion> Milestones { get; set; }
    }
}