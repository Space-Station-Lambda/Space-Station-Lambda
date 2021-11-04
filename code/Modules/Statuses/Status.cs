using Sandbox;
using ssl.Player;

namespace ssl.Modules.Statuses
{
    /// <summary>
    /// A status is something a player can have and perform each ticks
    /// </summary>
    public abstract partial class Status : BaseNetworkable
    {
        public float InitialTime = 0f;

        public float TimeLeft = 0f;

        /// <summary>
        /// Create a status with a specific duration
        /// </summary>
        /// <param name="duration">Set the duration of the status.</param>
        protected Status()
        {
        }

        /// <summary>
        /// Create a status with a specific duration
        /// </summary>
        /// <param name="duration">Set the duration of the status.</param>
        protected Status(float duration)
        {
            TimeLeft = duration;
            InitialTime = duration;
        }

        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string IconPath => "";
        public abstract bool IsInfinite { get; }

        /// <summary>
        /// When the status is applied to the player.
        /// </summary>
        /// <param name="player">The player who receive the status.</param>
        public virtual void OnApply(Player.Player player)
        {
        }

        /// <summary>
        /// When the status is removed from the player.
        /// </summary>
        /// <param name="player">The player who lost the status.</param>
        public virtual void OnResolve(Player.Player player)
        {
        }

        /// <summary>
        /// When the timer is finished.
        /// </summary>
        /// <param name="player">When the status timer is finished.</param>
        public virtual void OnEnd(Player.Player player)
        {
            OnResolve(player);
        }

        /// <summary>
        /// Each tick the status is on the player
        /// </summary>
        /// <param name="player">The player who is affected</param>
        public virtual void OnTick(Player.Player player)
        {
            if (!IsInfinite)
            {
                TimeLeft -= Time.Delta;
                CheckResolve(player);
            }
        }

        /// <summary>
        /// Check if the status can be removed
        /// </summary>
        /// <param name="player"></param>
        public virtual void CheckResolve(Player.Player player)
        {
            if (TimeLeft < 0)
            {
                //TODO Resolve ??
            }
        }
    }
}