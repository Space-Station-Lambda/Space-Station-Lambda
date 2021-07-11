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
            textLabel = Add.Label("", "round_label");
            timeLabel = Add.Label("", "round_time");
        }
        
        public override void Tick()
        {
            base.Tick();
            BaseRound currentRound = SslGame.Instance.RoundManager?.CurrentRound;
            if (currentRound == null) return;
            textLabel.Text = $"{currentRound.RoundName}:{currentRound.RoundEndTime}     ";
            timeLabel.Text = currentRound.TimeLeftFormatted;
            //_timeLabel.SetClass("hide", isWaitingRound);
        }
    }
}