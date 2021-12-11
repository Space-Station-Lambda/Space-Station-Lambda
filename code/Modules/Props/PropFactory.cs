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
		Prop prop;
		string type = propData.GetTypeId();
		
		switch ( type )
		{
			case Identifiers.Machine:
				PropMachineData propMachineData = (PropMachineData)propData;
				prop = new PropMachine
				{
					Complexity = propMachineData.Complexity,
					Durability = propMachineData.Durability,
				};
				break;
			case Identifiers.Bucket:
				prop = new PropBucket();
				break;
			case Identifiers.TrashBin:
				prop = new PropTrashBin();
				break;
			default:
				prop = new Prop();
				break;
		}

		prop.Id = propData.Id;
		prop.Name = propData.Name;
		prop.Model = propData.Model;
		return prop;
	}
}
