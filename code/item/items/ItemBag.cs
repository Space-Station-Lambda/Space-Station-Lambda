using SSL.player;

namespace SSL.item.items
{
    public class ItemBag : Item
    {
        public ItemBag(string id, string name, bool destroyOnUse = false) : base(id, name, "bag", 1, destroyOnUse) { }

        public override void Use(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}