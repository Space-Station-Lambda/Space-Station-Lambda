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
            if (data.SlipProbability is < 0f or > 1f)
                throw new ArgumentException("Slip probability out of bounds, must be in [0;1] (included).");
            SlipProbability = data.SlipProbability;
        }
        
        public float SlipProbability { get; private set; }

        public override void StartTouch(Entity other)
        {
            if (other is not Player.SslPlayer player) return;

            float random = (float)new Random().NextDouble();

            if (random <= SlipProbability)
            {
                player.RagdollHandler.StartRagdoll();
            }
        }
    }
}