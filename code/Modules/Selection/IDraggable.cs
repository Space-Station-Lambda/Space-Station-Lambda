using ssl.Player;

namespace ssl.Modules.Selection
{
    public interface IDraggable
    {
        /// <summary>
        /// When the drag started.
        /// </summary>
        /// <param name="player">The player who drags.</param>
        void OnDragStart(MainPlayer player);

        /// <summary>
        /// When the drag stopped.
        /// </summary>
        /// <param name="player">The player who drags.</param>
        void OnDragStop(MainPlayer player);

        /// <summary>
        /// While the entity is dragged.
        /// </summary>
        /// <param name="player">The player who drags.</param>
        void OnDrag(MainPlayer player);

        /// <summary>
        /// Check if the Draggable can be dragged by the player
        /// </summary>
        /// <param name="player">The player who wants to drag.</param>
        bool IsDraggable(MainPlayer player);
    }
}