using System;

namespace ssl.Modules.Rounds.Requirements;

/// <summary>
///     Base class for a round requirement. Used to define conditions before a round starts.
/// </summary>
public abstract class BaseRequirement
{
    public event Action RequirementFulfilled;

    /// <summary>
    ///     Returns true when the requirement is fulfilled. (i.e The amount of player is reached)
    /// </summary>
    public bool IsFulfilled => CheckIfFulfilled();

    public abstract void RegisterListeners();
    public abstract void UnregisterListeners();
    
    protected abstract bool CheckIfFulfilled();

    protected void TriggerFulfillmentEvent()
    {
        if (CheckIfFulfilled()) RequirementFulfilled?.Invoke();
    }
}