using System.Collections.Generic;
using ssl.Commons;
using ssl.Modules.Props.Data;

namespace ssl.Modules.Props;

public class PropDao : LocalDao<PropData>
{

	protected override void LoadAll()
	{
		Log.Info("Load props..");

		// Items
		Save(new PropData("ballon")
		{
			Name = "Ballon",
			Model = "models/citizen_props/balloonheart01.vmdl_c"
		});
		
		Save(new PropData("bucket")
		{
			Name = "Bucket",
			Model = "models/props/bucket/bucket.vmdl"
		});
		
		Save(new PropData("stain")
		{
			Name = "Stain",
			Model = ""
		});
		
		Save(new PropData("wet_floor_sign")
		{
			Name = "Wet Floor sign",
			Model = "models/props/wet_floor_sign/wet_floor_sign.vmdl",
			IsPhysical = true
		});
		
		Log.Info($"{All.Count} props charged !");
	}
}
