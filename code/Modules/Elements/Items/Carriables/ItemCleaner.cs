using ssl.Modules.Elements.Items.Data;
using ssl.Modules.Elements.Props.Types;
using ssl.Modules.Elements.Props.Types.Stains;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Elements.Items.Carriables
{
    public partial class ItemCleaner : Item<ItemCleanerData>
    {

	    /// <summary>
        /// The dirtyness is the level of sanity of the cleaner. 0 Is perfect and the max is to be defined.
        /// </summary>
        public int Dirtyness { get; private set; }
        public int CleaningValue { get; set; }
        public override void OnUsePrimary(SslPlayer sslPlayer, ISelectable target)
        {
            switch (target)
            {
                case Bucket bucket:
                    Wash(bucket);
                    break;
                default:
                    target.OnInteract(sslPlayer, CleaningValue);
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
        
    }
}
