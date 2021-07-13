using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Rounds;

namespace ssl.UI
{
    public class RoundInfos : Panel
    {
        private Label textLabel;
        private Label timeLabel;

        public RoundInfos()
        {
            StyleSheet.Load( "ui/roundinfos.scss" );
            textLabel = Add.Label("", "round_label");
            timeLabel = Add.Label("", "round_time");
        }

        public override void Tick()
        {
            base.Tick();
            BaseRound currentRound = SslGame.Instance.RoundManager?.CurrentRound;
            if (currentRound == null) return;
            textLabel.Text = "[" + currentRound.RoundName + "]";
            timeLabel.Text = "Temps Restant:" + currentRound.TimeLeftFormatted;
        }
    }
}