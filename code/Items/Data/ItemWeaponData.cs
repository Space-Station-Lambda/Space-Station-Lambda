using ItemWeapon = ssl.Items.Carriables.ItemWeapon;

namespace ssl.Items.Data
{
    public class ItemWeaponData : ItemData
    {
        public ItemWeaponData(string id, string name, string model, float primaryRate) : base(id, name, model)
        {
            PrimaryRate = primaryRate;
        }

        public float PrimaryRate { get; }
        
        public override ItemWeapon Create()
        {
            return new ItemWeapon(this);
        }
    }
}