using System;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Props.Data;
using ssl.Modules.Props.Instances;

namespace ssl.Modules.Props;

public class PropFactory : IFactory<Prop>
{
	private static PropFactory instance;

	private PropFactory()
	{
	}

	public static PropFactory Instance => instance ??= new PropFactory();

	public Prop Create( string id )
	{
		PropData propData = PropDao.Instance.FindById(id);

		Prop prop = propData switch
		{
			PropMachineData propMachineData => new PropMachine
			{
				Complexity = propMachineData.Complexity,
				Durability = propMachineData.Durability,
			},
			PropBucketData propBucketData => new PropBucket(),
			PropTrashBinData propTrashBinData => new PropTrashBin(),
			_ => new Prop()
		};

		prop.Id = propData.Id;
		prop.Name = propData.Name;
		prop.Model = propData.Model;
		return prop;
	}
}
