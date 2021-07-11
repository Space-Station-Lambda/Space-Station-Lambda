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

            textLabel.Text = $"{SslGame.Instance.RoundManager?.CurrentRound.RoundName}:";

            //_timeLabel.SetClass("hide", isWaitingRound);
        }
    }
}