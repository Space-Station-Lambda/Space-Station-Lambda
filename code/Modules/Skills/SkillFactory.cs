using System;
using ssl.Commons;
using ssl.Modules.Items;
using ssl.Modules.Items.Data;
using ssl.Modules.Items.Instances;
using ssl.Modules.Skills.Types;

namespace ssl.Modules.Skills;

public sealed class SkillFactory : IFactory<Skill>
{
    private static SkillFactory instance;

    private SkillFactory()
    {
    }

    public static SkillFactory Instance => instance ??= new SkillFactory();

    public Skill Create( string id )
    {
        SkillData skillData = SkillDao.Instance.FindById(id);
		
        Skill skill;
        string type = skillData.GetTypeId();

        switch (type)
        {
            case "fighting":
	            skill = new Strength();
	            break;
            default:
                throw new ArgumentException($"[Skills] No skill data found for {id}");
        }

        return skill;
    }
}
