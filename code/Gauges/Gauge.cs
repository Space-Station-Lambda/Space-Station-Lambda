using System;

namespace ssl.Gauges
{
    /// <summary>
    /// Like an HP bar, a gauge contains data wich move over time
    /// </summary>
    public class Gauge
    {
        public Gauge(GaugeData gaugeData)
        {
            GaugeData = gaugeData;
        }

        public GaugeData GaugeData { get; }

        /// <summary>
        /// Current Value
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Remaining value
        /// </summary>
        public int ValueLeft => GaugeData.MaxValue - Value;

        /// <summary>
        /// Add value to the gauge
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <exception cref="Exception">Out of gauge.</exception>
        public void AddValue(int value)
        {
            if (Value + value > GaugeData.MaxValue || Value + value < GaugeData.MinValue)
            {
                throw new Exception("Out of gauge exception.");
            }

            Value += value;
        }

        public override string ToString()
        {
            return $"[{GaugeData.Id}] {Value}/{GaugeData.MaxValue}";
        }
    }
}