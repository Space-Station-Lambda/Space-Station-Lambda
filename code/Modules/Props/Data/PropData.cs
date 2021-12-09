using ssl.Commons;
using ssl.Constants;

namespace ssl.Modules.Props.Data;

public class PropData : BaseData
{
	public PropData( string id ) : base($"{Identifiers.Prop}{Identifiers.Separator}{id}")
	{
	}
	
	/// <summary>
	/// Name of the prop.
	/// <example>Crate</example>
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// Model of the prop
	/// </summary>
	public string Model { get; set; }
	
	/// <summary>
	/// TODO: documentation
	/// </summary>
	public bool IsPhysical { get; set; }
}
