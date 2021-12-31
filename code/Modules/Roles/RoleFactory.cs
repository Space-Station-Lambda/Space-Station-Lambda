using ssl.Commons;
using ssl.Modules.Roles.Data;
using ssl.Modules.Roles.Instances;
using ssl.Modules.Skills;

namespace ssl.Modules.Roles;

public class RoleFactory : IFactory<Role>
{
    private static RoleFactory instance;

    private RoleFactory() { }

    public static RoleFactory Instance => instance ??= new RoleFactory();

    public Role Create(string id)
    {
        RoleData roleData = RoleDao.Instance.FindById(id);
        Role role = roleData switch
        {
            RoleTraitorData roleTraitorData => new Traitor(),
            _ => new Role()
        };

        role.Id = roleData.Id;
        role.Name = roleData.Name;
        role.Description = roleData.Description;
        role.Type = roleData.Type;
        role.Model = roleData.Model;
        role.Clothing = roleData.Clothing;
        role.StartingItems = roleData.StartingItems;
        role.Available = roleData.Available;

        foreach ((string skillId, int level) in roleData.Skills)
        {
            Skill skill = SkillFactory.Instance.Create(skillId);
            skill.Level = level;
            role.Skills.Add(skillId, skill);
        }

        return role;
    }
}