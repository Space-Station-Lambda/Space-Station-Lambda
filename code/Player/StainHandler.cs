﻿using System;
using Sandbox;
using ssl.Modules.Props;
using Prop = ssl.Modules.Props.Types.Prop;

namespace ssl.Player
{
    public class StainHandler
    {
        private const float StainChance = 0.002f;
        private readonly MainPlayer player;
        private const string StainId = "stain.step";
        
        public StainHandler(MainPlayer player)
        {
            this.player = player;
        }

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
            stain.Position = player.Position;
        }
    }
}