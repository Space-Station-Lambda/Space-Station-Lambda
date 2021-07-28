using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Assistant : Role
    {
        public override string Id => "assistant";
        public override string Name => "Assistant";
        public override string Description => "Assistant";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/hair/hair_femalebun.blonde.vmdl",
            "models/citizen_clothes/dress/dress.kneelength.vmdl",
            "models/citizen_clothes/accessory/pearlearrings.vmdl",
            "models/citizen_clothes/shoes/shoes_heels.vmdl"
        };
    }
}