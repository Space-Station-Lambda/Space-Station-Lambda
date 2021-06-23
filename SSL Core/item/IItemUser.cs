using SSL_Core.item.items;

namespace SSL_Core.item
{
    /// <summary>
    /// Interface that should be implemented on all entities that is able to use Items
    /// </summary>
    public interface IItemUser
    {
        //TODO: Add info on item use (position of use, target entity, ...)
        void Use(Item item);
    }
}