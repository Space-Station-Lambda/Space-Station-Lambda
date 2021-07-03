namespace SSL.Item.items
{
    public class ItemBase : Item
    {
        public ItemBase(string id, string name, int maxStack = 1) : base(id, name, "base", maxStack)
        {
        }

        public override void Use(Player.Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}