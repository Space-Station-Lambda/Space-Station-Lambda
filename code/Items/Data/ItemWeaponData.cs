using ssl.Items.Data.Weapon;

namespace ssl.Items.Data
{
    public class ItemWeaponData : ItemData
    {
        public ItemWeaponData(string id, string name, string model, float primaryRate) : base(id, name, model)
        {
            PrimaryRate = primaryRate;
        }

        public float PrimaryRate { get; }
        
        public override ItemWeapon create()
        {
            return new ItemWeapon(this);
        }
    }
}