using ssl.Roles;

namespace ssl.Rounds
{
    public class ScenarioConstraint
    {
        public ScenarioConstraint(Role role, int min = -1, int max = -1)
        {
            Role = role;
            Min = min;
            Max = max;
        }
        /// <summary>
        /// Role to constraint
        /// </summary>
        public Role Role { get; }
        /// <summary>
        /// Minimum amount of this role, -1 means infinite.
        /// </summary>
        public int Min { get; }
        /// <summary>
        /// Maximum amount of this role, -1 means infinite.
        /// </summary>
        public int Max { get; }

        public override string ToString()
        {
            return $"[{Role}]{Min}|{Max}";
        }
    }
}