using System.Collections.Generic;

namespace SSL_Core.model.player.gauges
{
    public class GaugeData
    {
        public string Id { get; }
        public int MinValue { get; } = 0;
        public int MaxValue { get; } = 100;
       

        public GaugeData(string id, int minValue, int maxValue)
        {
            Id = id;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}