using System;
using System.Collections.Generic;
using SSL_Core.model.gauges;
using SSL_Core.model.interfaces;
using SSL_Core.model.item;
using SSL_Core.model.roles;
using SSL_Core.model.status;

namespace SSL_Core.model.player
{
    public partial class Player : IEffectable<Player>
    {
        private const int InitialCapacity = 100;

        public Role Role;
        
        public StatusHandler<Player> StatusHandler { get; }

        public Inventory Inventory { get; }

        private Dictionary<string, Gauge> gaugeValues;
        
        public Player()
        {
            StatusHandler = new StatusHandler<Player>();
            //Inventory = new Inventory(InitialCapacity);
            gaugeValues = new Dictionary<string, Gauge>();
        }

        public Gauge GetGauge(string gaugeId)
        {
            gaugeValues.TryGetValue(gaugeId, out Gauge gauge);
            return gauge;
        }

        public void Apply(IEffect<Player> effect)
        {
            effect.Trigger(this);
        }
    }
}
