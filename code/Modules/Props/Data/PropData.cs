using ssl.Commons;
using ssl.Constants;

namespace ssl.Modules.Props.Data;

public class PropData : BaseData
{
	public PropData( string id ) : base(id)
	{ }
	
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
	/// If the prop is a physical he can be moved
	/// </summary>
	public bool IsPhysical { get; set; }
}
