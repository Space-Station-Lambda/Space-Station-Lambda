using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Skills;

public class SkillHandler : EntityComponent<SslPlayer>
{
    public bool TrySkillCheck(string skillId, int complexity)
    {
        Skill skill = GetSkills().ContainsKey(skillId) ? GetSkills()[skillId] : null;
        if (null == skill) return false;

        //Basic skill check
        Random r = new();
        //Roll between [1, skill.Level]
        int roll = r.Next(0, skill.Level) + 1;
        Log.Info($"[Skill]<Check for {complexity}> {skill.Name} : {roll}/{skill.Level} ! {roll >= complexity}");
        return roll >= complexity;
    }

    public Dictionary<string, Skill> GetSkills()
    {
        return Entity.RoleHandler.Role.Skills;
    }
}