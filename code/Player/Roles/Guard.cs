using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Guard : Role
    {
        public override string Id => "guard";
        public override string Name => "Guard";
        public override string Description => "Guard";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/hat/hat_hardhat.vmdl"
        };
    }
}