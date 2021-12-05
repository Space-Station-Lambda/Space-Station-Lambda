namespace ssl.Modules.Milestones;

/// <summary>
///     A milestone is an objetive for a faction
/// </summary>
public abstract class Milestone
{
	/// <summary>
	///     Unique Identifier
	/// </summary>
	public virtual string Id => "milestone.base";

	/// <summary>
	///     Name displayed
	/// </summary>
	public virtual string Name => "Milestone";

	/// <summary>
	///     Description displayed
	/// </summary>
	public virtual string Description => "A basic Milestone";

	/// <summary>
	///     Number of points
	/// </summary>
	public virtual int Points => 0;

	/// <summary>
	///     If true, the milestone is hidden for everybody
	/// </summary>
	public virtual HideType Hide => HideType.Show;

	//TODO Factions concerned

	/// <summary>
	///     Checks if the milestone is complete
	/// </summary>
	public abstract void CheckCompletion();
}
