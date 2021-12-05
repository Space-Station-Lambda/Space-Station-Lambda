namespace ssl.Modules.Milestones;

/// <summary>
///     A milestone completion is a milestone completed
/// </summary>
public class MilestoneCompletion
{
	public MilestoneCompletion( Milestone milestone )
	{
		Milestone = milestone;
		Completion = 0.0f;
	}

	/// <summary>
	///     MilestoneConcerned
	/// </summary>
	public Milestone Milestone { get; set; }

	/// <summary>
	///     Completion between 0 and 1
	/// </summary>
	public float Completion { get; set; }
}
