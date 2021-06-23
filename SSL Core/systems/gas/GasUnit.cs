using System;

namespace SSL_Core.systems.gas
{
    public class GasUnit
    {
        public int Value = 0;

        
        public void Randomize(int seed = 0)
        {
            Random rnd = new(seed);
            Value = rnd.Next(100);
        }
        
        public override string ToString()
        {
            return Value.ToString("00");
        }
    }
}