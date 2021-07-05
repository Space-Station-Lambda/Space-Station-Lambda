using ssl.Player;

namespace ssl.Item.ItemTypes
{
    public class ItemBag : ItemCore
    {
        public ItemBag(string id, string name, bool destroyOnUse = false) : base(id, name, "bag", 1, destroyOnUse)
        {
        }

        public override void UsedBy(MainPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}