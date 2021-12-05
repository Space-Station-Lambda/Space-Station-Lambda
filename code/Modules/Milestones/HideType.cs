namespace ssl.Modules.Milestones;

public enum HideType
{
	/// <summary>
	///     Displayed to everybody
	/// </summary>
	Show,

	/// <summary>
	///     Displayed in the end round
	/// </summary>
	Hidden,

	/// <summary>
	///     Hidden until completed
	/// </summary>
	Secret,

	/// <summary>
	///     Shared to the faction
	/// </summary>
	FactionShow,

	/// <summary>
	///     Hidden for the faction until completed
	/// </summary>
	FactionSecret
}
