using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemClothes : Item
    {

        public ItemClothes(ItemClothesData data) : base(data)
        {
            slot = data.Slot;
        }

        private ClothesSlot slot;
        

        public override void UsedBy(MainPlayer player)
        {
            player.ClothesHandler.AttachClothes(Model, slot);
            //TODO Give to the player the old cloth in this slot
        }
    }
}