using Sandbox;

namespace ssl.Modules.Commands
{
    public class RoundCommands
    {
        [AdminCmd("rrestart")]
        public static void RestartRound()
        {
            Gamemode.Instance.RoundManager.ChangeRound(Gamemode.Instance.RoundManager.CurrentRound);
        }

        [AdminCmd("rend")]
        public static void RoundEnd()
        {
            Gamemode.Instance.RoundManager.CurrentRound.Finish();
        }
        
        [AdminCmd("rset")]
        public static void RoundSetTimer(int timer)
        {
            Gamemode.Instance.RoundManager.CurrentRound.RoundEndTime = Time.Now + timer;
        }
        
        [AdminCmd("rtime")]
        public static void RoundGetTimer()
        {
            Log.Info(Gamemode.Instance.RoundManager.CurrentRound.TimeLeftFormatted);
        }
    }
}