using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Traitor : Role
    {
        public override string Id => "traitor";
        public override string Name => "Traitor";
        public override string Description => "Traitor";

        public override HashSet<string> Clothing => new();
    }
}