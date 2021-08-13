using System;
using ssl.Player;

namespace ssl
{
    public class PlayerRemovedEventArgs : EventArgs
    {
        public MainPlayer Player { get; set; }
    }
}