using System;
using ssl.Factories;
using ssl.Modules.Props.Data;
using ssl.Modules.Props.Instances;

namespace ssl.Modules.Props;

public class PropFactory : IFactory<Prop>
{
	private static PropFactory instance;
	private PropDao propDao = new();

	private PropFactory()
	{
	}

	public static PropFactory Instance => instance ??= new PropFactory();

	public Prop Create( string id )
	{
		PropData propData = propDao.FindById(id);

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
				throw new ArgumentException("Item type not supported"); 
		}

		prop.Id = propData.Id;
		prop.Name = propData.Name;
		prop.Model = propData.Model;
		return prop;
	}
}
