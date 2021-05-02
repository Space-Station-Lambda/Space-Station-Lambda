using SSL_Core.model.player;

namespace SSL_Core.model.item.items
{
    public class ItemBase : Item
    {
        public ItemBase(string id, string name, int maxStack = 1) : base(id, name, "base", maxStack)
        {
        }

        public override void Use(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}