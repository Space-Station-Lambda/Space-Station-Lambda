using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemBag : Item
    {
        public override string Id => "item.bag";
        public override string Name => "Bag";
        public override int MaxStack => 1;
        public override bool DestroyOnUse => false;
        public override string Model => "";

        public override void UsedBy(MainPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}