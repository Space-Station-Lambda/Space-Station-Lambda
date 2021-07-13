using System.Collections.Generic;

namespace ssl.Systems.Atmosphere
{
    public interface IGasMovementStrategy
    {
        GasMovement GenerateGasMovement(AtmosphericUnit source, IEnumerable<AtmosphericUnit> neighbors);
    }
}