using Sandbox;

namespace ssl.Commands
{
    public static class TestCommands
    {
        [AdminCmd]
        public static void ping()
        {
            Log.Info("Pong");
        }
    }
}