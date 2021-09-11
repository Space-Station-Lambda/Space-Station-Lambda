using ssl.Player;

namespace ssl.Modules.Statuses.Types
{
    public partial class Sickness : Status
    {
        private const byte TargetRColor = 140;
        private const byte TargetGColor = 207;
        private const byte TargetBColor = 100;


        public Sickness() : base()
        {
        }

        public Sickness(int duration) : base(duration)
        {
        }

        public override string Id => "sick";
        public override string Name => "Sick";
        public override string Description => "Sick";
        public override bool IsInfinite => false;

        public override void OnTick(MainPlayer player)
        {
            base.OnTick(player);
            float sicknessRatio = (InitialTime - TimeLeft) / InitialTime;
            byte r = (byte)(255 - (sicknessRatio * 255 - TargetRColor));
            byte g = (byte)(255 - (sicknessRatio * 255 - TargetGColor));
            byte b = (byte)(255 - (sicknessRatio * 255 - TargetBColor));
            player.RenderColor = new Color(r, g, b);
        }

        public override void OnResolve(MainPlayer player)
        {
            player.RenderColor = Color.White;
            base.OnResolve(player);
        }
    }
}