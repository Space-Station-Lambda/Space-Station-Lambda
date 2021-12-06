using System.Collections.Generic;

namespace ssl.Data;

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
	
	/// <summary>
	/// List of clothing
	/// TODO create "skin" system with a SkinData class
	/// </summary>
	public IEnumerable<string> Clothing { get; set; }
	
	/// <summary>
	/// List of starting items
	/// </summary>
	public IEnumerable<string> StartingItems { get; set; }
	
	//TODO Skill system

}
