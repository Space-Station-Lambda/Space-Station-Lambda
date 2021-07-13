using System;

namespace ssl.Systems.Atmosphere
{
    public class AtmosphericUnit
    {
        public int Value;


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