using System.Collections.Generic;
using ssl.Commons;
using ssl.Constants;

namespace ssl.Modules.Roles.Data;

public class RoleData : BaseData
{
	public RoleData( string id ) : base($"{Identifiers.Skill}{Identifiers.Separator}{id}")
	{ }
	
	/// <summary>
	/// Name of the role
	/// <example>Assistant</example>
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// Description of the role
	/// <example>The assistant is the couteau suisse of the station</example>
	/// </summary>
	public string Description { get; set; }

	public string Type { get; set; } = Identifiers.Protagonist;
	
	/// <summary>
	/// Model of the role
	/// </summary>
	public string Model { get; set; } = "models/units/simpleterry.vmdl";
	
	/// <summary>
	/// List of clothing
	/// TODO create "skin" system with a SkinData class
	/// </summary>
	public IEnumerable<string> Clothing { get; set; }= new HashSet<string>();

	

	/// <summary>
	/// List of starting items
	/// </summary>
	public IEnumerable<string> StartingItems { get; set; } = new HashSet<string>();

	public Dictionary<string, int> Skills { get; set; } = new();

}
