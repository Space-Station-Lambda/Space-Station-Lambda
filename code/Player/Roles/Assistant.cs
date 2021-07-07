using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Assistant : Role
    {
        public override string Id => "role.assistant";
        public override string Name => "Assistant";
        public override string Description => "Assistant";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/hat/hat_hardhat.vmdl"
        };
    }
}