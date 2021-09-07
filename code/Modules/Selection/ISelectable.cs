using ssl.Modules.Items.Carriables;
using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Selection
{
    public interface ISelectable
    {
        /// <summary>
        /// When the selection started.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelectStart(MainPlayer player);
        /// <summary>
        /// When the slection stopped.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelectStop(MainPlayer player);
        /// <summary>
        /// While the is selected.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelect(MainPlayer player);

        /// <summary>
        /// Trigger when the action is requested by the player
        /// </summary>
        /// <param name="player">The player who perform the action.</param>
        public void OnAction(MainPlayer player, Item item);
    }
}