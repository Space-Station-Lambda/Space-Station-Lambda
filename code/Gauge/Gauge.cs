using SSL.Exceptions;

namespace SSL.Gauge
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
        
        /// <summary>
        /// Ajoute la valeur à la gauge actuelle.
        /// </summary>
        /// <param name="value">Valeur a ajouter. Peut être négative pour décroitre la gauge.</param>
        /// <exception cref="OutOfGaugeException">Exception levée si nous sortons des limites de la gauge.</exception>
        public void AddValue(int value)
        {
            if (Value + value > GaugeData.MaxValue || Value + value < GaugeData.MinValue)
            {
                throw new OutOfGaugeException();
            }
            
            Value += value;
        }
        
        public override string ToString() 
        {
            return $"[{GaugeData.Id}] {Value}/{GaugeData.MaxValue}";
        }
        
    }
}