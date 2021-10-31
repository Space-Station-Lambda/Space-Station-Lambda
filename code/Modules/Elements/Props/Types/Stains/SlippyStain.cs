using Sandbox;
using ssl.Modules.Elements.Props.Data.Stains;
using ssl.Player;

namespace ssl.Modules.Elements.Props.Types.Stains
{
    public class SlippyStain : Stain
    {
        public SlippyStain(SlippyStainData data) : base(data)
        {
            SlipProbability = data.SlipProbability;
        }
        
        public float SlipProbability { get; private set; }

        public override void StartTouch(Entity other)
        {
            if (other is not MainPlayer player) return;
            
            player.RagdollHandler.StartRagdoll();
            player.RagdollHandler.TimeExitRagdoll = Time.Now;
        }
    }
}