namespace ssl.Modules.Skills
{
    public class SkillLevel
    {
        public SkillLevel(Skill skill, int level)
        {
            this.Skill = skill;
            Level = level;
        }

        private Skill Skill { get; }
        public int Level { get; set; }
    }
}