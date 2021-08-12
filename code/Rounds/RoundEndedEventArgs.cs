using System;

namespace ssl.Rounds
{
    public class RoundEndedEventArgs : EventArgs
    {
        public BaseRound Round { get; set; }
    }
}