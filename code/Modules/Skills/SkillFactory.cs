using ssl.Commons;

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

        Skill skill = skillData switch
        {
	        _ => new Skill()
        };

        return skill;
    }
}
