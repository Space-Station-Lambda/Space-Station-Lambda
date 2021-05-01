using System.Collections.Generic;
using SSL_Core.model.gauges;
using SSL_Core.model.interfaces;
using SSL_Core.model.roles;
using SSL_Core.model.status;

namespace SSL_Core.model.player
{
    public partial class Player : IEffectable
    {
        private const int InitialCapacity = 100;

        public Role Role;
        
        private Dictionary<string, Gauge> gaugeValues;

        private StatusHandler<Player> statusHandler;
        
        public Inventory Inventory { get; }
        
        public Player()
        {
            Inventory = new Inventory(InitialCapacity);
            gaugeValues = new Dictionary<string, Gauge>();
            statusHandler = new StatusHandler<Player>();
        }

        public Gauge GetGauge(string gaugeId)
        {
            gaugeValues.TryGetValue(gaugeId, out Gauge gauge);
            return gauge;
        }

    }
}
