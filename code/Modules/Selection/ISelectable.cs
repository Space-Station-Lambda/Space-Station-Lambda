using ssl.Player;

namespace ssl.Modules.Selection
{
    public interface ISelectable
    {
        /// <summary>
        /// When the selection started.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelectStart(Player.Player player);

        /// <summary>
        /// When the slection stopped.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelectStop(Player.Player player);

        /// <summary>
        /// While the is selected.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelect(Player.Player player);

        /// <summary>
        /// Trigger when the action is requested by the player
        /// </summary>
        /// <param name="player">The player who perform the action.</param>
        public void OnInteract(Player.Player player);
    }
}