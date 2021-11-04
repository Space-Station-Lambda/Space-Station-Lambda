﻿using ssl.Player;

namespace ssl.Modules.Statuses.Types
{
    public class Sickness : Status
    {
        private const byte TargetRColor = 140;
        private const byte TargetGColor = 207;
        private const byte TargetBColor = 100;


        public Sickness(int duration) : base(duration)
        {
        }

        public override string Id => "status.sickness";
        public override string Name => "Sickness";
        public override string Description => "Sickness";
        public override bool IsInfinite => false;

        public override void OnTick(Player.Player player)
        {
            base.OnTick(player);
            float sicknessRatio = (InitialTime - TimeLeft) / InitialTime;
            byte r = (byte)(255 - (sicknessRatio * 255 - TargetRColor));
            byte g = (byte)(255 - (sicknessRatio * 255 - TargetGColor));
            byte b = (byte)(255 - (sicknessRatio * 255 - TargetBColor));
            player.RenderColor = new Color(r, g, b);
        }

        public override void OnResolve(Player.Player player)
        {
            player.RenderColor = Color.White;
            base.OnResolve(player);
        }
    }
}