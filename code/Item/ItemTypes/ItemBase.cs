using ssl.Player;

namespace ssl.Item.ItemTypes
{
    public class ItemBase : ItemCore
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