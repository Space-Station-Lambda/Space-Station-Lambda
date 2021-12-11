using System.Collections.Generic;

namespace ssl.Modules.Skills;

/// <summary>
///     A specific skill value. We don't use the experience system for this because wed don't really want to exp.
/// </summary>
public class Skill
{
	public string Id { get; }
	public string Name { get; }
	public string Description { get; }
	public int Level { get; set; } = 0;
}
