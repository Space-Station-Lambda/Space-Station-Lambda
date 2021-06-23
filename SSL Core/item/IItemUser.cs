using SSL_Core.item.items;

namespace SSL_Core.item
{
    public interface IItemUser
    {
        //TODO: Add info on item use (position of use, target entity, ...)
        void Use(Item item);
    }
}