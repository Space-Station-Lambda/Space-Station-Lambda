using ssl.Modules.Elements.Items.Data;
using ssl.Modules.Elements.Props.Types;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Elements.Items.Carriables
{
    public partial class ItemCleaner : Item<ItemCleanerData>
    {

        
        
        
        public ItemCleaner()
        {
        }

        public ItemCleaner(ItemCleanerData itemData) : base(itemData)
        {
        }
        
        /// <summary>
        /// The dirtyness is the level of sanity of the cleaner. 0 Is perfect and the max is to be defined.
        /// </summary>
        public int Dirtyness { get; private set; }
        
        public override void OnUsePrimary(MainPlayer player, ISelectable target)
        {
            switch (target)
            {
                case Stain stain:
                    Clean(stain);
                    break;
                case Bucket bucket:
                    Wash(bucket);
                    break;
            }
        }

        /// <summary>
        /// wash the cleaner in a bucket for set the dirty level to 0.
        /// </summary>
        /// <param name="bucket"></param>
        private void Wash(Bucket bucket)
        {
            bucket.WasteWater(Dirtyness);
            Dirtyness = 0;
        }

        /// <summary>
        /// Clean stain with the cleaning value. The cleaning have to be decreased by the current dirtyness.
        /// </summary>
        private void Clean(Stain stain)
        {
            stain.CleanStain(Data.CleaningValue);
            Dirtyness++;
        }
    }
}