using System.Collections.Generic;

namespace ssl.Modules.Roles.Types.Jobs
{
    public class Engineer : Job
    {
        public override string Id => "engineer";
        public override string Name => "Engineer";
        public override string Description => "Engineer";

        public override IEnumerable<string> Clothing => new HashSet<string>
        {
            "models/citizen_clothes/trousers/trousers.lab.vmdl",
            "models/citizen_clothes/shirt/shirt_longsleeve.plain.vmdl",
            "models/citizen_clothes/shoes/shoes.police.vmdl",
            "models/citizen_clothes/hat/hat_hardhat.vmdl"
        };

        public override IEnumerable<string> Items => new List<string>
        {
        };
    }
}