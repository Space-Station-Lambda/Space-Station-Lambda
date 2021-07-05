using System.Collections.Generic;

namespace ssl.Systems.gas
{
    public interface IGasMovementStrategy
    {
        GasMovement GenerateGasMovement(GasUnit source, List<GasUnit> neighbors);
    }
}