using ssl.Modules.Clothes;
using ssl.Modules.Items.Data;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemClothes : Item
    {
        private ClothesSlot slot;

        public ItemClothes()
        {
        }

        public ItemClothes(ItemClothesData data) : base(data)
        {
            slot = data.Slot;
        }

        public override void UseOn(MainPlayer player)
        {
            player.ClothesHandler.AttachClothes(Model, slot);
            //TODO Give to the player the old cloth in this slot
        }
    }
}