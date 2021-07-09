using System.Collections.Generic;

namespace ssl.Systems.gas
{
    public interface IGasMovementStrategy
    {
        GasMovement GenerateGasMovement(AtmosUnit source, IEnumerable<AtmosUnit> neighbors);
    }
}