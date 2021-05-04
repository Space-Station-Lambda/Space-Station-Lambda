using System.Runtime.Serialization;

namespace SSL_Core.model.gauges
{
    [DataContract(Name = "Gauge")]
    public class GaugeData
    {
        [DataMember]
        public string Id { get; }
        [DataMember]
        public int MinValue { get; } = 0;
        [DataMember]
        public int MaxValue { get; } = 100;
       

        public GaugeData(string id, int minValue, int maxValue)
        {
            Id = id;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}