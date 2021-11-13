using Sandbox;
using ssl.Modules.Elements.Props.Data.Stains;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Elements.Props.Types.Stains
{
    public class Stain : Prop<StainData>, IWorkable
    {
        public int RemainingWork { get; set; } = 10;
        
        public Stain()
        {
        }

        public Stain(StainData itemData) : base(itemData)
        {
            CollisionGroup = CollisionGroup.Trigger;
        }

        public void OnWorkDone(SslPlayer player)
        {
            Delete();
        }
    }
}