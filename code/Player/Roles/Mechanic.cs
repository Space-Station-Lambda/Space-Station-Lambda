using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Mechanic : Role
    {
        public override string Id => "mechanic";
        public override string Name => "Mechanic";
        public override string Description => "Mechanic";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/hat/hat_hardhat.vmdl"
        };
    }
}