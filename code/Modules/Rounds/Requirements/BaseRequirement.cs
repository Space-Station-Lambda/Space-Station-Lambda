namespace ssl.Modules.Rounds.Requirements;

/// <summary>
///     Base class for a round requirement. Used to define conditions before a round starts.
/// </summary>
public abstract class BaseRequirement
{
    /// <summary>
    ///     Returns true when the requirement is fulfilled. (i.e The amount of player is reached)
    /// </summary>
    public abstract bool IsFulfilled { get; }
}