﻿using Sandbox;
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
        /// <param name="sslPlayer">The player who receive the status.</param>
        public virtual void OnApply(Player.SslPlayer sslPlayer)
        {
        }

        /// <summary>
        /// When the status is removed from the player.
        /// </summary>
        /// <param name="sslPlayer">The player who lost the status.</param>
        public virtual void OnResolve(Player.SslPlayer sslPlayer)
        {
        }

        /// <summary>
        /// When the timer is finished.
        /// </summary>
        /// <param name="sslPlayer">When the status timer is finished.</param>
        public virtual void OnEnd(Player.SslPlayer sslPlayer)
        {
            OnResolve(sslPlayer);
        }

        /// <summary>
        /// Each tick the status is on the player
        /// </summary>
        /// <param name="sslPlayer">The player who is affected</param>
        public virtual void OnTick(Player.SslPlayer sslPlayer)
        {
            if (!IsInfinite)
            {
                TimeLeft -= Time.Delta;
                CheckResolve(sslPlayer);
            }
        }

        /// <summary>
        /// Check if the status can be removed
        /// </summary>
        /// <param name="sslPlayer"></param>
        public virtual void CheckResolve(Player.SslPlayer sslPlayer)
        {
            if (TimeLeft < 0)
            {
                //TODO Resolve ??
            }
        }
    }
}