using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Captain : Role
    {
        public override string Id => "captain";
        public override string Name => "Captain";
        public override string Description => "Captain";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/trousers/smarttrousers/smarttrousers.vmdl",
            "models/citizen_clothes/shoes/shoes.police.vmdl",
            "models/citizen_clothes/jacket/suitjacket/suitjacketunbuttonedshirt.vmdl",
            "models/citizen_clothes/hair/hair_malestyle02.vmdl"
        };
    }
}