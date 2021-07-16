using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public class ItemStackWeapon : ItemStack
    {
        public BaseWeapon Weapon;
        
        public ItemStackWeapon(ItemWeapon item, int amount = 1) : base(item, amount)
        {
            Weapon = new BaseWeapon();
        }
    }
}