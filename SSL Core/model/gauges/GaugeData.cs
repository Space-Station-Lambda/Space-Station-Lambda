namespace SSL_Core.model.gauges
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
        
          public override string ToString() 
        {
            return $"{Id}";
        }

    }
}