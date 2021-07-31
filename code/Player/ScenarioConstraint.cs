using ssl.Player.Roles;

namespace ssl.Player
{
    public class ScenarioConstraint
    {
        public ScenarioConstraint(Role role, int min, int max)
        {
            Role = role;
            Min = min;
            Max = max;
        }

        public Role Role { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}