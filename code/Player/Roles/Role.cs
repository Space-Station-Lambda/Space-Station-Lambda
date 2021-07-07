using System.Collections.Generic;

namespace ssl.Player.Roles
{
    /// <summary>
    /// Player's role
    /// </summary>
    public abstract class Role
    {
        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string Model => "models/units/simpleterry.vmdl";
        public abstract HashSet<string> Clothing { get; }

        public override string ToString()
        {
            return $"[{Id}]{Name}";
        }
    }
}