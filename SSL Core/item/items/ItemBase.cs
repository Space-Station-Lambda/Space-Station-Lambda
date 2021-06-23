using SSL_Core.player;

namespace SSL_Core.item.items
{
    public class ItemBase : Item
    {
        public ItemBase(string id, string name, int maxStack = 1) : base(id, name, "base", maxStack)
        {
        }

        public override void UsedBy(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}