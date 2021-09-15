using ssl.Modules.Items.Data;
using ssl.Modules.Props.Types;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemCleaner : Item<ItemCleanerData>
    {
        
        public ItemCleaner()
        {
        }

        public ItemCleaner(ItemCleanerData itemData) : base(itemData)
        {
        }
        
        public override void OnUsePrimary(MainPlayer player, ISelectable target)
        {
            if (target is Stain stain)
            {
                //Clean stain with the cleaning value
                stain.CleanStain(Data.CleaningValue);
            }
        }
    }
}