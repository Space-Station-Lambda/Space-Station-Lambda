using System;

namespace SSL_Core.model.status
{
    public class StatusFinishedEventArgs : EventArgs
    {
        public float SecondsElapsed { get; }

        public StatusFinishedEventArgs(float secondsElapsed)
        {
            SecondsElapsed = secondsElapsed;
        }
    }
}