using ssl.Player;

namespace ssl.item.items
{
    public class ItemBase : Item
    {
        public ItemBase(string id, string name, int maxStack = 1) : base(id, name, "base", maxStack)
        {
        }

        public override void UsedBy(MainPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}