using System;
using ssl.Dao;
using ssl.Data;
using ssl.Factories;
using ssl.Modules.Items;
using ssl.Modules.Items.Data;
using ssl.Modules.Items.Instances;

namespace ssl.Modules.Skills;

public sealed class SkillFactory : IFactory<Skill>
{
    private static SkillFactory instance;
    private readonly SkillDao skillDao = new();

    private SkillFactory()
    {
    }

    public static SkillFactory Instance => instance ??= new SkillFactory();

    public Skill Create( string id )
    {
        SkillData skillData = skillDao.FindById(id);
		
        Skill skill;
        string type = skillData.GetTypeId();

        switch (type)
        {
            
            default:
                throw new ArgumentException($"[Skills] No skill data found for {id}");
        }

        return skill;
    }
}