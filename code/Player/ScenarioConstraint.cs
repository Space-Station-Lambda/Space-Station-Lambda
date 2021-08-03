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

        public Role Role { get; }
        public int Min { get; }
        public int Max { get; }

        public override string ToString()
        {
            return $"[{Role}]{Min}|{Max}";
        }
    }
}