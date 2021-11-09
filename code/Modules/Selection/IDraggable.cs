using ssl.Player;

namespace ssl.Modules.Selection
{
    public interface IDraggable : ISelectable
    {
        /// <summary>
        /// When the drag started.
        /// </summary>
        /// <param name="sslPlayer">The player who drags.</param>
        void OnDragStart(SslPlayer sslPlayer);

        /// <summary>
        /// When the drag stopped.
        /// </summary>
        /// <param name="sslPlayer">The player who drags.</param>
        void OnDragStop(SslPlayer sslPlayer);

        /// <summary>
        /// While the entity is dragged.
        /// </summary>
        /// <param name="sslPlayer">The player who drags.</param>
        void OnDrag(SslPlayer sslPlayer);

        /// <summary>
        /// Check if the Draggable can be dragged by the player
        /// </summary>
        /// <param name="sslPlayer">The player who wants to drag.</param>
        bool IsDraggable(SslPlayer sslPlayer);
    }
}