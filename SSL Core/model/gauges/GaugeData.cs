using System.Runtime.Serialization;

namespace SSL_Core.model.gauges
{
    [DataContract(Name = "Gauge")]
    public class GaugeData
    {
        [DataMember]
        public string Id { get; private set;  }
        [DataMember]
        public int MinValue { get; private set; } = 0;
        [DataMember]
        public int MaxValue { get; private set; } = 100;

        private GaugeData()
        {
            
        }
        public GaugeData(string id, int minValue, int maxValue)
        {
            Id = id;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}