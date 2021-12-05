using System.Collections.Generic;

namespace ssl.Modules.Roles.Types.Jobs;

public class Captain : Job
{
	public override string Id => "captain";
	public override string Name => "Captain";
	public override string Description => "Captain";

	public override IEnumerable<string> Clothing => new HashSet<string>
	{
		"models/citizen_clothes/trousers/smarttrousers/smarttrousers.vmdl",
		"models/citizen_clothes/shoes/shoes.police.vmdl",
		"models/citizen_clothes/jacket/suitjacket/suitjacketunbuttonedshirt.vmdl",
		"models/citizen_clothes/hair/hair_malestyle02.vmdl"
	};
}
