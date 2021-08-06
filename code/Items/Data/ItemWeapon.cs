using ssl.Player;

namespace ssl.Items.Data
{
    public class ItemWeapon : Item
    {
        public ItemWeapon(string id, string name, string model) : base(id, name, model)
        {
        }

        public override void UseOn(MainPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}