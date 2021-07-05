namespace ssl.Gauge
{
    public class GaugeData
    {
        private GaugeData()
        {
        }

        public GaugeData(string id, int minValue = 0, int maxValue = 100)
        {
            Id = id;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public string Id { get; private set; }
        public int MinValue { get; private set; } = 0;
        public int MaxValue { get; private set; } = 100;

        public override string ToString()
        {
            return $"[{Id}] {MinValue}|{MaxValue}";
        }
    }
}