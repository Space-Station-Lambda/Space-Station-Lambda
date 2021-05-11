using SSL_Core.exception;

namespace SSL_Core.model.gauges
{
    public class Gauge
    {
        public GaugeData GaugeData { get; }
        public int Value { get; private set;  }
        public int ValueLeft => GaugeData.MaxValue - Value;
       
        public Gauge(GaugeData gaugeData)
        {
            GaugeData = gaugeData;
        }
        
        public void AddValue(int value)
        {
            Value += value;
                    
            if (Value > GaugeData.MaxValue || Value < GaugeData.MinValue)
            {
                throw new OutOfGaugeException();
            }
        }
        
        public override string ToString() 
        {
            return $"[{GaugeData}] {Value}/{GaugeData.MaxValue}";
        }
        
    }
}