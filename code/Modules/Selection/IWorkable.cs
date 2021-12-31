using ssl.Player;

namespace ssl.Modules.Selection;

/// <summary>
///     Used if the specific class represents something with an amount of "work" to do. For example if we need to repair
///     something it is not instantaneous and it needs some work.
/// </summary>
public interface IWorkable : ISelectable
{
	/// <summary>
	///     Remaining work
	/// </summary>
	public int RemainingWork { get; set; }

	/// <summary>
	///     Triggered when the work is done and the RemainingWork is 0.
	/// </summary>
	public void OnWorkDone(SslPlayer player);

    public new void OnInteract(SslPlayer player, int strength)
    {
        RemainingWork -= strength;
        if (RemainingWork <= 0) OnWorkDone(player);
    }
}