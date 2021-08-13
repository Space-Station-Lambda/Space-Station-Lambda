using System.Collections.Generic;

namespace ssl.Modules.Roles.Types.Jobs
{
    public class Scientist : Job
    {
        public override string Id => "scientist";
        public override string Name => "Scientist";
        public override string Description => "Scientist";

        public override IEnumerable<string> Clothing => new HashSet<string>
        {
            "models/citizen_clothes/jacket/labcoat.vmdl",
            "models/citizen_clothes/gloves/gloves_workgloves.vmdl",
            "models/citizen_clothes/trousers/trousers.lab.vmdl",
            "models/citizen_clothes/shoes/shoes.workboots.vmdl"
        };
    }
}