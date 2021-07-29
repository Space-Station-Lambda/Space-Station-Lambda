using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Mechanic : Role
    {
        public override string Id => "mechanic";
        public override string Name => "Mechanic";
        public override string Description => "Mechanic";

        public override IEnumerable<string> Clothing => new HashSet<string>
        {
            "models/citizen_clothes/shoes/shoes.workboots.vmdl",
            "models/citizen_clothes/shoes/shorts.cargo.vmdl",
            "models/citizen_clothes/hat/hat_hardhat.vmdl",
            "models/citizen_clothes/gloves/gloves_workgloves.vmdl",
            "models/citizen_clothes/jacket/jacket.red.vmdl"
        };
    }
}