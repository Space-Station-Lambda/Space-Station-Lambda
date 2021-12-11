using System;
using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Roles.Data;
using ssl.Modules.Roles.Instances;
using ssl.Modules.Scenarios;

namespace ssl.Modules.Roles;

public class RoleFactory : IFactory<Role>
{
	private static RoleFactory instance;

	private RoleFactory()
	{
	}
	
	public static RoleFactory Instance => instance ??= new RoleFactory();
	public Role Create( string id )
	{
		RoleData roleData = RoleDao.Instance.FindById(id);
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
