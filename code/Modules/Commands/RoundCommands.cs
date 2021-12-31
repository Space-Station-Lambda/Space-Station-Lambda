using Sandbox;

namespace ssl.Modules.Commands;

public class RoundCommands
{
    [AdminCmd("rn_start")]
    public static void RestartRound()
    {
        Gamemode.Instance.RoundManager.ChangeRound(Gamemode.Instance.RoundManager.CurrentRound);
    }

    [AdminCmd("rn_end")]
    public static void RoundEnd()
    {
        Gamemode.Instance.RoundManager.CurrentRound.Finish();
    }

    [AdminCmd("rn_set")]
    public static void RoundSetTimer(int timer)
    {
        Gamemode.Instance.RoundManager.CurrentRound.RoundEndTime = Time.Now + timer;
    }

    [AdminCmd("rn_time")]
    public static void RoundGetTimer()
    {
        Log.Info(Gamemode.Instance.RoundManager.CurrentRound.TimeLeft);
    }
}