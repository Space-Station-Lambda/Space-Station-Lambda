using ssl.Modules.Clothes;
using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items.Data
{
    public class ItemClothesData : ItemData
    {
        public ItemClothesData(string id, string name, string model, ClothesSlot slot) : base(id, name, model)
        {
            Slot = slot;
        }

        public ClothesSlot Slot { get; }

        public override ItemClothes Create()
        {
            return new(this);
        }
    }
}