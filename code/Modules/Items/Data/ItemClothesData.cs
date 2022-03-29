using ssl.Modules.Clothes;

namespace ssl.Modules.Items.Data;

public class ItemClothesData : ItemData
{
    public ItemClothesData(string id) : base(id) { }
    
    public ClothesSlot Slot { get; set; }
}