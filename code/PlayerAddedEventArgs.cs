using System;
using ssl.Player;

namespace ssl
{
    public class PlayerAddedEventArgs : EventArgs
    {
        public MainPlayer Player { get; set; }
    }
}