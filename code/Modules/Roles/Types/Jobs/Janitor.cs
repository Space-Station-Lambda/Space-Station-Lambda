using System.Collections.Generic;

namespace ssl.Modules.Roles.Types.Jobs;

public class Janitor : Job
{
	public override string Id => "janitor";
	public override string Name => "Janitor";
	public override string Description => "Janitor";

	public override IEnumerable<string> Clothing => new HashSet<string>
	{
		"models/citizen_clothes/gloves/gloves_workgloves.vmdl",
		"models/citizen_clothes/shirt/shirt_longsleeve.plain.vmdl",
		"models/citizen_clothes/trousers/trousers.jeans.vmdl",
		"models/citizen_clothes/shoes/shoes.workboots.vmdl",
		"models/citizen_clothes/hat/hat_service.vmdl"
	};

	public override IEnumerable<string> Items => new List<string> {"item.mop", "item.sponge", "item.cleaning_spray"};
}
