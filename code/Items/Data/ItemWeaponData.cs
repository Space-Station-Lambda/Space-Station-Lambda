using ItemWeapon = ssl.Items.Carriables.ItemWeapon;

namespace ssl.Items.Data
{
    /// <summary>
    /// Data of the item weapon
    /// </summary>
    public class ItemWeaponData : ItemData
    {
        public ItemWeaponData(string id, string name, string model, float primaryRate, float range = 0f) : base(id, name, model)
        {
            PrimaryRate = primaryRate;
            Range = range;
        }
        /// <summary>
        /// Rate of fire
        /// </summary>
        public float PrimaryRate { get; }
        /// <summary>
        /// Range of the weapon, 0 means max range
        /// </summary>
        public float Range { get; }
        
        public override ItemWeapon Create()
        {
            return new ItemWeapon(this);
        }
    }
}