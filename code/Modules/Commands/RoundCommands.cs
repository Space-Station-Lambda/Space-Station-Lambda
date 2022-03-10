using Sandbox;

namespace ssl.Modules.Commands;

public class RoundCommands
{
    [AdminCmd("rn_start")]
    public static void RestartRound()
    {
        Gamemode.Current.RoundManager.ChangeRound(Gamemode.Current.RoundManager.CurrentRound);
    }

    [AdminCmd("rn_end")]
    public static void RoundEnd()
    {
        Gamemode.Current.RoundManager.CurrentRound.Finish();
    }

    [AdminCmd("rn_set")]
    public static void RoundSetTimer(int timer)
    {
        Gamemode.Current.RoundManager.CurrentRound.RoundEndTime = Time.Now + timer;
    }

    [AdminCmd("rn_time")]
    public static void RoundGetTimer()
    {
        Log.Info(Gamemode.Current.RoundManager.CurrentRound.TimeLeft);
    }
}