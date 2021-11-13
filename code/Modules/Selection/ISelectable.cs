using ssl.Player;

namespace ssl.Modules.Selection
{
    public interface ISelectable
    {
        /// <summary>
        /// When the selection started.
        /// </summary>
        /// <param name="sslPlayer">The player who select.</param>
        public void OnSelectStart(SslPlayer sslPlayer);

        /// <summary>
        /// When the slection stopped.
        /// </summary>
        /// <param name="sslPlayer">The player who select.</param>
        public void OnSelectStop(SslPlayer sslPlayer);

        /// <summary>
        /// While the is selected.
        /// </summary>
        /// <param name="sslPlayer">The player who select.</param>
        public void OnSelect(SslPlayer sslPlayer);

        /// <summary>
        /// Trigger when the action is requested by the player
        /// </summary>
        /// <param name="sslPlayer">The player who perform the action.</param>
        /// <param name="strength">The power of the usage. For example if the player is skilled, he use more easily</param>
        public void OnInteract(SslPlayer sslPlayer, int strength);
    }
}