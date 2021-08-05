using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemClothesData : ItemData
    {
        public ItemClothesData(string id, string name, string model, ClothesSlot slot) : base(id, name, model)
        {
            Slot = slot;
        }

        public ClothesSlot Slot { get; }
        
        public override Item create()
        {
            return new ItemClothes(this);
        }
    }
}