using Sandbox;
using ssl.Items.Data;

namespace ssl.Items
{
    public class ItemStackWeapon : ItemStack
    {
        public BaseWeapon Weapon;

        public ItemStackWeapon(ItemWeapon item) : base(item)
        {
            Weapon = new BaseWeapon();
        }
    }
}