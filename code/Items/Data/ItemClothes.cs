using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemClothes : Item
    {

        public ItemClothes(string id, string name, string model, ClothesSlot slot) : base(id, name)
        {
            Model = model;
            this.slot = slot;
        }

        private ClothesSlot slot;


        public override void UsedBy(MainPlayer player, ItemStack itemStack)
        {
            player.ClothesHandler.AttachClothes(Model, slot);
            //TODO Give to the player the old cloth in this slot
        }
    }
}