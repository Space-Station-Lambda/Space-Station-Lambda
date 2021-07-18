using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemClothes : Item
    {
        public ItemClothes(string id, string name, string model) : base(id, name)
        {
            Model = model;
        }

        public override int MaxStack => 1;
        public override bool DestroyOnUse => true;

        public override void UsedBy(MainPlayer player)
        {
            player.ClothesHandler.AttachClothes(Model, false);
            //TODO Give to the player the old cloth in this slot
        }
    }
}