using System.Collections.Generic;

namespace ssl.Roles
{
    public class Captain : Role
    {
        public override string Id => "captain";
        public override string Name => "Captain";
        public override string Description => "Captain";

        public override IEnumerable<string> Clothing => new HashSet<string>
        {
            "models/citizen_clothes/trousers/smarttrousers/smarttrousers.vmdl",
            "models/citizen_clothes/shoes/shoes.police.vmdl",
            "models/citizen_clothes/jacket/suitjacket/suitjacketunbuttonedshirt.vmdl",
            "models/citizen_clothes/hair/hair_malestyle02.vmdl"
        };
    }
}