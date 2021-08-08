using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Items;

namespace ssl.Player.Roles
{
    public class Traitor : Role
    {
        public override string Id => "traitor";
        public override string Name => "Traitor";
        public override string Description => "Traitor";

        public override IEnumerable<string> Clothing => new HashSet<string>
        {
            "models/citizen_clothes/trousers/trousers.smart.vmdl",
            "models/citizen_clothes/shoes/shoes.police.vmdl",
            "models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
            "models/citizen_clothes/hat/hat_beret.black.vmdl"
        };

        public override IEnumerable<string> Items => new List<string>
        {
           "weapon.knife"
        };

        public Role SecondaryRole { get; set; }

        public override void OnAssigned(MainPlayer player)
        {
            base.OnAssigned(player);
            player.RoleHandler.SetPreference(new Traitor(), RolePreference.Never);
            SecondaryRole = player.RoleHandler.GetRandomRoleFromPreferences();
            SecondaryRole.OnAssigned(player);
        }

        public override void OnSpawn(MainPlayer player)
        {
            base.OnSpawn(player);
            SecondaryRole.OnSpawn(player);
        }

        public override void OnUnassigned(MainPlayer player)
        {
            base.OnUnassigned(player);
            SecondaryRole.OnUnassigned(player);
        }
    }
}