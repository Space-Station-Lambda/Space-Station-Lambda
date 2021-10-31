using System;
using Sandbox;
using ssl.Modules.Elements.Props;
using Prop = ssl.Modules.Elements.Props.Types.Prop;

namespace ssl.Player
{
    public class StainHandler : EntityComponent
    {
        private const float StainChance = 0.002f;
        private const string StainId = "stain.step";
        

        /// <summary>
        ///  Spawn a stain with a specific probability
        /// </summary>
        public void TryGenerateStain()
        {
            float prob = Time.Delta * StainChance;
            double r = new Random().NextDouble();
            if (prob > r)
            {
                GenerateStain();
            }
        }

        private void GenerateStain()
        {
            PropFactory factory = new();
            Prop stain = factory.Create(StainId);
            stain.Position = Entity.Position;
        }
    }
}