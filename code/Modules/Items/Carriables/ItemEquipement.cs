using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemEquipement : Item<ItemData>
    {
        public ItemEquipement()
        {
        }

        public ItemEquipement(ItemData itemData) : base(itemData)
        {
        }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void OnUsePrimary(MainPlayer player, ISelectable target)
        {
            //TODO equip the item
        }
    }
}