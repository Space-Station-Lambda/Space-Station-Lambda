using System.Collections.Generic;

namespace ssl.Gauge
{
    public class GaugeHandler
    {
        private Dictionary<string, Gauge> gauges;

        public GaugeHandler()
        {
            gauges = new Dictionary<string, Gauge>();
        }

        public void AddGauge(Gauge gauge)
        {
            gauges.Add(gauge.GaugeData.Id, gauge);
        }

        public void AddGauge(GaugeData gaugeData)
        {
            gauges.Add(gaugeData.Id, new Gauge(gaugeData));
        }

        public Gauge GetGauge(string gaugeId)
        {
            return gauges[gaugeId];
        }

        public int GetGaugeValue(string gaugeId)
        {
            return gauges[gaugeId].Value;
        }

        /// <summary>
        /// Ajoute la valeur donnée à la gauge.
        /// </summary>
        /// <param name="gaugeId">Identifiant de la gauge.</param>
        /// <param name="value">Valeur à ajouter. Peut être négative pour décroitre la gauge.</param>
        public void AddValueToGauge(string gaugeId, int value)
        {
            gauges[gaugeId].AddValue(value);
        }
    }
}