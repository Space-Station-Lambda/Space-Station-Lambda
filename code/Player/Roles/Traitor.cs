using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Traitor : Role
    {
        public override string Id => "traitor";
        public override string Name => "Traitor";
        public override string Description => "Traitor";
        public override HashSet<string> Clothing => new()
        {
	        "models/citizen_clothes/trousers/trousers.smart.vmdl",
	        "models/citizen_clothes/shoes/shoes.police.vmdl",
	        "models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
	        "models/citizen_clothes/hat/hat_beret.black.vmdl"
        };
    }
}