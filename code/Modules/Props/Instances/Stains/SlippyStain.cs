using System;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Props.Instances.Stains;

public class SlippyStain : Stain
{
    public float SlipProbability { get; set; }

    public override void StartTouch(Entity other)
    {
        if (other is not SslPlayer player) return;

        float random = (float) new Random().NextDouble();

        if (random <= SlipProbability) player.RagdollHandler.StartRagdoll();
    }
}