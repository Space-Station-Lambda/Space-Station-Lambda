using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Ghost : Role
    {
        public override string Id => "ghost";
        public override string Name => "Ghost";
        public override string Description => "Ghost";

        public override IEnumerable<string> Clothing => new HashSet<string>();
    }
}