using SSL.gauges;
using SSL.interfaces;
using SSL.roles;
using SSL.status;

namespace SSL.player
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
