namespace SSL.Item.items
{
    public class ItemBag : Item
    {
        public ItemBag(string id, string name, bool destroyOnUse = false) : base(id, name, "bag", 1, destroyOnUse) { }

        public override void Use(Player.Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}