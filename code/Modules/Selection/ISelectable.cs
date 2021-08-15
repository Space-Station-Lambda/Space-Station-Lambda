namespace ssl.Player
{
    public interface ISelectable
    {
        /// <summary>
        /// When the selection started.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelectStart(ISelector player);
        /// <summary>
        /// When the slection stopped.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelectStop(ISelector player);
        /// <summary>
        /// While the is selected.
        /// </summary>
        /// <param name="player">The player who select.</param>
        public void OnSelect(ISelector player);
        /// <summary>
        /// Trigger when the action is requested by the player
        /// </summary>
        /// <param name="player">The player who perform the action.</param>
        public void OnAction(ISelector player);
    }
}