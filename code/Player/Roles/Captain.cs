using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Captain : Role
    {
        public override string Id => "captain";
        public override string Name => "Captain";
        public override string Description => "Captain";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/hat/hat_hardhat.vmdl"
        };
    }
}