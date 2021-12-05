using System;
using ssl.Dao;
using ssl.Modules.Props.Instances;

namespace ssl.Factories;

public class PropFactory : IFactory<Prop>
{
	private static PropFactory instance;
	private PropDao itemDao = new();

	private PropFactory()
	{
	}

	public static PropFactory Instance => instance ??= new PropFactory();

	public Prop Create( string id )
	{
		throw new NotImplementedException();
	}
}
