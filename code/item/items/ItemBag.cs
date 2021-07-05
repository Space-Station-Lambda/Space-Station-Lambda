using SSL.Player;

namespace SSL.Item.items
{
    public class ItemBag : Item
    {
        public ItemBag(string id, string name, bool destroyOnUse = false) : base(id, name, "bag", 1, destroyOnUse)
        {
        }

        public override void Use(MainPlayer mainPlayer)
        {
            throw new System.NotImplementedException();
        }
    }
}