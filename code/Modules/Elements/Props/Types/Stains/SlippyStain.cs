using System;
using Sandbox;
using ssl.Modules.Elements.Props.Data.Stains;
using ssl.Player;

namespace ssl.Modules.Elements.Props.Types.Stains
{
    public class SlippyStain : Stain
    {
        public SlippyStain(SlippyStainData data) : base(data)
        {
            SlipProbability = data.SlipProbability.Clamp(0f, 1f);
        }
        
        public float SlipProbability { get; private set; }

        public override void StartTouch(Entity other)
        {
            if (other is not MainPlayer player) return;

            float random = (float)new Random().NextDouble();

            if (random <= SlipProbability)
            {
                player.RagdollHandler.StartRagdoll();
            }
        }
    }
}