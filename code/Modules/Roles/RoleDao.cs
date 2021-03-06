using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Roles.Data;

namespace ssl.Modules.Roles;

public class RoleDao : LocalDao<RoleData>
{
    private static RoleDao instance;

    private RoleDao() { }

    public static RoleDao Instance => instance ??= new RoleDao();

    protected override void LoadAll()
    {
        Log.Info("Load roles...");

        Save(new RoleData(Identifiers.Roles.ASSISTANT_ID)
        {
            Available = true,
            Name = "Assistant",
            Description = "An assistant is a role who assists other roles in tasks.",
            Clothing = new HashSet<string>(),
            StartingItems = new HashSet<string>
            {
                Identifiers.Items.APPLE_ID
            },
            Skills = new Dictionary<string, int>
            {
                {
                    Identifiers.Skills.CLEANING_ID,
                    30
                },
                {
                    Identifiers.Skills.SPEED_ID,
                    60
                }
            }
        });

        Save(new RoleData(Identifiers.Roles.JANITOR_ID)
        {
            Available = true,
            Name = "Janitor",
            Description = "Janitor",
            Clothing = new HashSet<string>
            {
                Identifiers.Items.WORKGLOVES_ID,
                Identifiers.Items.RED_LONGSLEEVE_ID,
                Identifiers.Items.JEANS_ID,
                Identifiers.Items.WORKBOOTS_ID,
                Identifiers.Items.SERVICE_HAT_ID
            },
            StartingItems = new HashSet<string>
            {
            },
            Skills = new Dictionary<string, int>
            {
                {
                    Identifiers.Skills.CLEANING_ID,
                    99
                },
                {
                    Identifiers.Skills.SPEED_ID,
                    50
                }
            }
        });

        Save(new RoleData(Identifiers.Roles.GUARD_ID)
        {
            Available = true,
            Name = "Guard",
            Description = "Guard",
            Clothing = new HashSet<string>
            {
                Identifiers.Items.GUARD_HAT_ID,
                Identifiers.Items.GUARD_SLEEVE_ID,
                Identifiers.Items.GUARD_TROUSERS_ID,
                Identifiers.Items.GUARD_SHOES_ID
            },
            StartingItems = new HashSet<string>
            {
                Identifiers.Items.PISTOL_ID,
                Identifiers.Items.HANDCUFFS_ID
            },
            Skills = new Dictionary<string, int>
            {
                {
                    Identifiers.Skills.STRENGTH_ID,
                    50
                },
                {
                    Identifiers.Skills.SPEED_ID,
                    40
                }
            }
        });

        //Save(new RoleData(Identifiers.Roles.TRAITOR_ID)
        //{
        //    Name = "Traitor",
        //    Description = "Traitor",
        //    Type = Identifiers.Roles.ANTAGONIST_TYPE,
        //    Clothing = new HashSet<string>
        //    {
        //        "models/citizen_clothes/trousers/trousers.smart.vmdl",
        //        "models/citizen_clothes/shoes/shoes.police.vmdl",
        //        "models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
        //        "models/citizen_clothes/hat/hat_beret.black.vmdl"
        //    }
        //});

        Save(new RoleData(Identifiers.Roles.GHOST_ID)
        {
            Name = "Ghost",
            Description = "Ghost",
            Type = Identifiers.Roles.OTHER_TYPE,
            Clothing = new HashSet<string>()
        });
        /*
        Save(new RoleData(Identifiers.CAPTAIN_ID)
        {
            Name = "Captain",
            Description = "The Captain is a role who leads the station.",
            Clothing = new HashSet<string>
            {
                "models/citizen_clothes/trousers/smarttrousers/smarttrousers.vmdl",
                "models/citizen_clothes/shoes/shoes.police.vmdl",
                "models/citizen_clothes/jacket/suitjacket/suitjacketunbuttonedshirt.vmdl",
                "models/citizen_clothes/hair/hair_malestyle02.vmdl"
            },
        });
        
        Save(new RoleData(Identifiers.ENGINEER_ID)
        {
            Name = "Engineer",
            Description = "The engineer is a role who repairs the station.",
            Clothing = new HashSet<string>
            {
                "models/citizen_clothes/trousers/smarttrousers/smarttrousers.vmdl",
                "models/citizen_clothes/shoes/shoes.police.vmdl",
                "models/citizen_clothes/jacket/suitjacket/suitjacketunbuttonedshirt.vmdl",
                "models/citizen_clothes/hair/hair_malestyle02.vmdl"
            },
            Skills = new Dictionary<string, int>
            {
                {
                    "skill.repair", 10
                }
            }
        });


        Save(new RoleData(Identifiers.SCIENTIST_ID)
        {
            Name = "Scientist",
            Description = "Scientist",
            Clothing = new HashSet<string>
            {
                "models/citizen_clothes/jacket/labcoat.vmdl",
                "models/citizen_clothes/gloves/gloves_workgloves.vmdl",
                "models/citizen_clothes/trousers/trousers.lab.vmdl",
                "models/citizen_clothes/shoes/shoes.workboots.vmdl"
            },
        });
        
        */
    }
}