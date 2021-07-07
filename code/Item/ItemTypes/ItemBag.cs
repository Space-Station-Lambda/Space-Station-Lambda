using ssl.Player;

namespace ssl.Item.ItemTypes
{
    public class ItemBag : ItemCore
    {
        public ItemBag(string id, string name, string model, bool destroyOnUse = false) : base(id, name, "bag", model, 1, destroyOnUse)
        {
        }

        public override void UsedBy(MainPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}