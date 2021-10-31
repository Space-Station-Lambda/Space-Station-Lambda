using System.Collections.Generic;

namespace ssl.Modules.Roles.Types.Jobs
{
    public class Assistant : Job
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

        public override IEnumerable<string> Items => new List<string>
        {
        };
    }
}