using ssl.Modules.Items.Carriables;
using ssl.Modules.Props.Data;
using ssl.Player;

namespace ssl.Modules.Props.Types
{
    public class Stain : Prop<PropData>
    {
        private const string MopId = "item.mop";
        private const string SpongeId = "item.sponge";
        
        public Stain()
        {
        }

        public Stain(PropData itemData) : base(itemData)
        {
        }

        public override void OnAction(MainPlayer player, Item item)
        {
            switch (item.Id)
            {
                case MopId:
                case SpongeId:
                    Delete();
                    break;
            }
        }
    }
}