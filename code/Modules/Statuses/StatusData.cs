using ssl.Commons;

namespace ssl.Modules.Statuses;

/// <summary>
/// Represents data of a status
/// </summary>
public class StatusData : BaseData
{
	public StatusData( string id ) : base(id)
	{ }
	
	/// <summary>
	/// Name of the status
	/// <example>Sickness</example>
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// Description of the status
	/// <example>You are thick</example>
	/// </summary>
	public string Description { get; set; }
	
	/// <summary>
	/// Duration of the status in miliseconds.
	/// 0 means infinite.
	/// </summary>
	public float Duration { get; set; }
}
