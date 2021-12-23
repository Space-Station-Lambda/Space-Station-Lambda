using ssl.Commons;

namespace ssl.Modules.Skills;

public class SkillData : BaseData
{
	public SkillData( string id ) : base(id)
	{
	}
	
	public string Name { get; set; }
	public string Description { get; set; }
}
