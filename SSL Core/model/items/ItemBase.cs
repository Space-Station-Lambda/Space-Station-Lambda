namespace SSL_Core.model.items
{
    public class ItemBase : Item
    {
        public ItemBase(string id, string name, int maxStack = 1) : base(id, name, maxStack)
        {
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}