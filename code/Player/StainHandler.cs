using System;
using Sandbox;
using ssl.Constants;
using ssl.Modules.Props;
using Prop = ssl.Modules.Props.Instances.Prop;

namespace ssl.Player;

public class StainHandler : EntityComponent
{
    private const float STAIN_CHANCE = 0.002f;

    /// <summary>
    ///     Spawn a stain with a specific probability
    /// </summary>
    public void TryGenerateStain()
    {
        float prob = Time.Delta * STAIN_CHANCE;
        double r = new Random().NextDouble();
        if (prob > r) GenerateStain();
    }

    private void GenerateStain()
    {
        PropFactory factory = PropFactory.Instance;
        Prop stain = factory.Create(Identifiers.STAIN_ID);
        stain.Position = Entity.Position;
    }
}