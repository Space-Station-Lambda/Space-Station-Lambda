using Sandbox;
using ssl.Player;

namespace ssl.Modules.Statuses;

/// <summary>
///     A status is something a player can have and perform each ticks
/// </summary>
public abstract class Status : BaseNetworkable
{
    public float InitialTime;

    public float TimeLeft;

    /// <summary>
    ///     Create a status with a specific duration
    /// </summary>
    protected Status() { }

    /// <summary>
    ///     Create a status with a specific duration
    /// </summary>
    /// <param name="duration">Set the duration of the status.</param>
    protected Status(float duration)
    {
        TimeLeft = duration;
        InitialTime = duration;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsInfinite { get; set; }
    public string IconPath => "";

    /// <summary>
    ///     When the status is applied to the player.
    /// </summary>
    /// <param name="sslPlayer">The player who receive the status.</param>
    public virtual void OnApply(SslPlayer sslPlayer) { }

    /// <summary>
    ///     When the status is removed from the player.
    /// </summary>
    /// <param name="sslPlayer">The player who lost the status.</param>
    public virtual void OnResolve(SslPlayer sslPlayer) { }

    /// <summary>
    ///     When the timer is finished.
    /// </summary>
    /// <param name="sslPlayer">When the status timer is finished.</param>
    public virtual void OnEnd(SslPlayer sslPlayer)
    {
        OnResolve(sslPlayer);
    }

    /// <summary>
    ///     Each tick the status is on the player
    /// </summary>
    /// <param name="sslPlayer">The player who is affected</param>
    public virtual void OnTick(SslPlayer sslPlayer)
    {
        if (!IsInfinite)
        {
            TimeLeft -= Time.Delta;
            CheckResolve(sslPlayer);
        }
    }

    /// <summary>
    ///     Check if the status can be removed
    /// </summary>
    /// <param name="sslPlayer"></param>
    public virtual void CheckResolve(SslPlayer sslPlayer)
    {
        if (TimeLeft < 0)
        {
            //TODO SSL-387:Resolve ??
        }
    }
}