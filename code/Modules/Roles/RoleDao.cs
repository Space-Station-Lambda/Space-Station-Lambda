using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Roles.Data;

namespace ssl.Modules.Roles;

public class RoleDao : LocalDao<RoleData>
{
	private static RoleDao instance;

	private RoleDao()
	{
	}
	
	public static RoleDao Instance => instance ??= new RoleDao();

	protected override void LoadAll()
	{
		Log.Info("Load roles...");
		
		Save(new RoleData(Identifiers.ASSISTANT_ID)
		{
			Name = "Assistant",
			Description = "An assistant is a role who assists other roles in tasks.",
			Clothing = new HashSet<string>
			{
				"models/citizen_clothes/hair/hair_femalebun.blonde.vmdl",
				"models/citizen_clothes/dress/dress.kneelength.vmdl",
				"models/citizen_clothes/shoes/trainers.vmdl_c"
			},
		});
		
		Save(new RoleData(Identifiers.JANITOR_ID)
		{
			Name = "Janitor",
			Description = "Janitor",
			Clothing = new HashSet<string>
			{
				"models/citizen_clothes/gloves/gloves_workgloves.vmdl",
				"models/citizen_clothes/shirt/shirt_longsleeve.plain.vmdl",
				"models/citizen_clothes/trousers/trousers.jeans.vmdl",
				"models/citizen_clothes/shoes/shoes.workboots.vmdl",
				"models/citizen_clothes/hat/hat_service.vmdl"
			},
			StartingItems = new HashSet<string>
			{
				"item.mop", 
				"item.sponge", 
				"item.cleaning_spray"
			}
		});

		Save(new RoleData(Identifiers.GUARD_ID)
		{
			Name = "Guard",
			Description = "Guard",
			Clothing = new HashSet<string>
			{
				"models/citizen_clothes/hat/hat_uniform.police.vmdl",
				"models/citizen_clothes/shirt/shirt_longsleeve.police.vmdl",
				"models/citizen_clothes/shoes/shoes.police.vmdl",
				"models/citizen_clothes/trousers/trousers.police.vmdl"
			},
		});
		
		Save(new RoleData(Identifiers.TRAITOR_ID)
		{
			Name = "Traitor",
			Description = "Traitor",
			Type = Identifiers.ANTAGONIST_TYPE,
			Clothing = new HashSet<string>
			{
				"models/citizen_clothes/trousers/trousers.smart.vmdl",
				"models/citizen_clothes/shoes/shoes.police.vmdl",
				"models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
				"models/citizen_clothes/hat/hat_beret.black.vmdl"
			}
		});
		
		Save(new RoleData(Identifiers.GHOST_ID)
		{
			Name = "Ghost",
			Description = "Ghost",
			Type = Identifiers.OTHER_TYPE,
			Clothing = new HashSet<string>
			{
				"models/citizen_clothes/trousers/trousers.smart.vmdl",
				"models/citizen_clothes/shoes/shoes.police.vmdl",
				"models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
				"models/citizen_clothes/hat/hat_beret.black.vmdl"
			}
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
