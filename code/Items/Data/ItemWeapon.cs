using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemWeapon : Item
    {
        public ItemWeapon(string id, string name) : base(id, name)
        {
        }

        public override void UsedBy(MainPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}