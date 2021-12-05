using System.Collections.Generic;

namespace ssl.Modules.Skills;

/// <summary>
///     Set of skills and their experience
/// </summary>
public class SkillSet
{
	/// <summary>
	///     We init all skills here for now. We don't really want to have to much skills.
	/// </summary>
	public SkillSet( params (string, int)[] initSkillLevels )
	{
		SkillLevels = new Dictionary<string, SkillLevel>();
		foreach ( (string key, Skill skill) in Skill.All )
		{
			// We set all skills to level 0
			SkillLevels.Add(key, new SkillLevel(skill, 0));
		}

		foreach ( (string id, int level) in initSkillLevels )
		{
			SkillLevels[id].Level = level;
		}
	}

	public Dictionary<string, SkillLevel> SkillLevels { get; set; }
}
