using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Janitor : Role
    {
        public override string Id => "janitor";
        public override string Name => "Janitor";
        public override string Description => "Janitor";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/gloves/gloves_workgloves.vmdl",
            "models/citizen_clothes/shirt/shirt_longsleeve.plain.vmdl",
            "models/citizen_clothes/trousers/trousers.jeans.vmdl",
            "models/citizen_clothes/shoes/shoes.workboots.vmdl",
            "models/citizen_clothes/hat/hat_service.vmdl",
        };
    }
}