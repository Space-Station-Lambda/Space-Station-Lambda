﻿using System.Collections.Generic;

namespace ssl.Player.Roles
{
    public class Scientist : Role
    {
        public override string Id => "role.scientist";
        public override string Name => "Scientist";
        public override string Description => "Scientist";

        public override HashSet<string> Clothing => new()
        {
            "models/citizen_clothes/jacket/labcoat.vmdl",
            "models/citizen_clothes/gloves/gloves_workgloves.vmdl",
            "models/citizen_clothes/trousers/trousers.lab.vmdl",
            "models/citizen_clothes/shoes/shoes.workboots.vmdl"
        };
    }
}