using System.Collections.Generic;
using SSL_Core.model.gauges;
using SSL_Core.model.roles;

namespace SSL_Core.model.player
{
    public partial class Player
    {
        private const int InitialCapacity = 100;

        public Role Role;
        
        private Dictionary<string, Gauge> gaugeValues;

        public Player()
        {
            //Inventory = new Inventory(InitialCapacity);
            gaugeValues = new Dictionary<string, Gauge>();
        }

        public Gauge GetGauge(string gaugeId)
        {
            gaugeValues.TryGetValue(gaugeId, out Gauge gauge);
            return gauge;
        }

    }
}
