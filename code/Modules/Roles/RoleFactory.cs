using ssl.Dao;
using ssl.Data;
using ssl.Factories;
using ssl.Modules.Scenarios;

namespace ssl.Modules.Roles;

public class RoleFactory : IFactory<Role>
{
	private static RoleFactory instance;
	private readonly RoleDao scenarioDao = new();

	private RoleFactory()
	{
	}

	public static RoleFactory Instance => instance ??= new RoleFactory();
	public Role Create( string id )
	{
		throw new System.NotImplementedException();
	}
}
