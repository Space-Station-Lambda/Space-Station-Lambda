using System.Collections.Generic;

namespace ssl.Systems.Atmosphere
{
    public interface IGasMovementStrategy
    {
        GasMovement GenerateGasMovement(AtmosUnit source, IEnumerable<AtmosUnit> neighbors);
    }
}