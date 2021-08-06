using ssl.Items.Data;
using ssl.Player;

namespace ssl.Items.Carriables
{
    public partial class ItemClothes : Item
    {
        public ItemClothes()
        {
        }

        public ItemClothes(ItemClothesData data) : base(data)
        {
            slot = data.Slot;
        }

        private ClothesSlot slot;
        
        public override void UsedOn(MainPlayer player)
        {
            player.ClothesHandler.AttachClothes(Model, slot);
            //TODO Give to the player the old cloth in this slot
        }
    }
}