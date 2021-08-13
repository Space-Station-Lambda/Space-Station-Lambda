using System.Collections.Generic;

namespace ssl.Roles
{
    public class Assistant : Role
    {
        public override string Id => "assistant";
        public override string Name => "Assistant";
        public override string Description => "Assistant";

        public override IEnumerable<string> Clothing => new HashSet<string>
        {
            "models/citizen_clothes/hair/hair_femalebun.blonde.vmdl",
            "models/citizen_clothes/dress/dress.kneelength.vmdl",
            "models/citizen_clothes/shoes/trainers.vmdl_c"
        };
    }
}