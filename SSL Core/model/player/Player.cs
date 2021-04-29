using System.Collections.Generic;
using SSL_Core.model.player.gauges;
using SSL_Core.model.roles;

namespace SSL_Core.model.player
{
    public partial class Player
    {
        private const int InitialCapacity = 100;

        public Role Role;
        
        private Dictionary<string, Gauge> gaugeValue;
        
        public Inventory Inventory { get; }
        
        public Player()
        {
            Inventory = new Inventory(InitialCapacity);
            gaugeValue = new Dictionary<string, Gauge>();
        }

        public Gauge GetGauge(string gaugeId)
        {
            gaugeValue.TryGetValue(gaugeId, out Gauge gauge);
            return gauge;
        }

    }
}
