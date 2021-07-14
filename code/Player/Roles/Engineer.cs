using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Engineer : Role
    {
        public override string Id => "engineer";
        public override string Name => "Engineer";
        public override string Description => "Engineer";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/hat/hat_hardhat.vmdl"
        };
    }
}