using System;
using ssl.Dao;
using ssl.Data;
using ssl.Factories;
using ssl.Modules.Roles.Types.Antagonists;
using ssl.Modules.Scenarios;

namespace ssl.Modules.Roles;

public class RoleFactory : IFactory<Role>
{
	private static RoleFactory instance;
	private readonly RoleDao roleDao = new();

	private RoleFactory()
	{
	}

	//TODO Remove this, we use this because he handle the dao who handle the data.
	//TODO In a perfect worl the data is separated from the dao.
	//TODO Makes the dao a singleton OR (preferably) don't store data in dao.
	[Obsolete]
	public string[] GetAllIds()
	{
		return roleDao.FindAllIds();
	}
	
	public static RoleFactory Instance => instance ??= new RoleFactory();
	public Role Create( string id )
	{
		RoleData roleData = roleDao.FindById(id);
		string roleName = roleData.GetBaseTypeId();
		Role role;
		switch ( roleName )
		{
			case Identifiers.Traitor:
				role = new Traitor();
				break;
			default:
				role = new Role();
				break;
		}

		role.Id = roleData.Id;
		role.Name = roleData.Name;
		role.Description = roleData.Description;
		role.Type = roleData.Type;
		role.Model = roleData.Model;
		role.Clothing = roleData.Clothing;
		role.StartingItems = roleData.StartingItems;
		//TODO SkillSet
		//TODO Faction
		return role;
	}
}
