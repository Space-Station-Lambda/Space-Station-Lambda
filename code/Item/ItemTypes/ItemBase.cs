using ssl.Player;

namespace ssl.item.ItemTypes
{
    public class ItemBase : ItemCore
    {
        public ItemBase(string id, string name, string model, int maxStack = 1) : base(id, name, "base", model, maxStack)
        {
        }

        public override void UsedBy(MainPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}