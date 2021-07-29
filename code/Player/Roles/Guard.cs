using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Guard : Role
    {
        public override string Id => "guard";
        public override string Name => "Guard";
        public override string Description => "Guard";

        public override IEnumerable<string> Clothing => new HashSet<string>
        {
	        "models/citizen_clothes/hat/hat_uniform.police.vmdl",
	        "models/citizen_clothes/shirt/shirt_longsleeve.police.vmdl",
	        "models/citizen_clothes/shoes/shoes.police.vmdl",
	        "models/citizen_clothes/trousers/trousers.police.vmdl"
        };
    }
}