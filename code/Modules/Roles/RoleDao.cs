using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Roles.Data;

namespace ssl.Modules.Roles;

public class RoleDao : LocalDao<RoleData>
{

	protected override void LoadAll()
	{
		Log.Info("Load roles...");
		
		Save(new RoleData(Identifiers.Assistant)
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
		
		Save(new RoleData(Identifiers.Captain)
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
		
		Save(new RoleData(Identifiers.Engineer)
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
		
		Save(new RoleData(Identifiers.Guard)
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
		
		Save(new RoleData(Identifiers.Janitor)
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
		
		
		Save(new RoleData(Identifiers.Scientist)
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
		
		Save(new RoleData(Identifiers.Traitor)
		{
			Name = "Traitor",
			Description = "Traitor",
			Type = Identifiers.Antagonist,
			Clothing = new HashSet<string>
			{
				"models/citizen_clothes/trousers/trousers.smart.vmdl",
				"models/citizen_clothes/shoes/shoes.police.vmdl",
				"models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
				"models/citizen_clothes/hat/hat_beret.black.vmdl"
			}
		});
		
		Save(new RoleData(Identifiers.Ghost)
		{
			Name = "Ghost",
			Description = "Ghost",
			Type = Identifiers.Other,
			Clothing = new HashSet<string>
			{
				"models/citizen_clothes/trousers/trousers.smart.vmdl",
				"models/citizen_clothes/shoes/shoes.police.vmdl",
				"models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
				"models/citizen_clothes/hat/hat_beret.black.vmdl"
			}
		});
	}
}
