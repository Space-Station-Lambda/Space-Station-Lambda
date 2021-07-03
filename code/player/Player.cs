using System.Collections.Generic;
using SSL_Core.gauges;
using SSL_Core.interfaces;
using SSL_Core.item;
using SSL_Core.roles;
using SSL_Core.status;

namespace SSL_Core.player
{
    public partial class Player : IEffectable<Player>
    {
        private const int InitialCapacity = 100;

        public Role Role;
        
        public StatusHandler<Player> StatusHandler { get; }
        public GaugeHandler GaugeHandler { get;  }

        public Player()
        {
            StatusHandler = new StatusHandler<Player>();
            GaugeHandler = new GaugeHandler();
        }
        
        public void Apply(IEffect<Player> effect)
        {
            effect.Trigger(this);
        }
    }
}
