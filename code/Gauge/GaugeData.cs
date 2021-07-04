using System.Runtime.Serialization;

namespace SSL.Gauge
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
        public GaugeData(string id, int minValue = 0, int maxValue = 100)
        {
            Id = id;
            MinValue = minValue;
            MaxValue = maxValue;
        }
        public override string ToString() 
        {
            return $"[{Id}] {MinValue}|{MaxValue}";
        }
    }
}