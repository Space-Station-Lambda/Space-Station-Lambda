using System.Collections.Generic;

namespace SSL_Core.systems.gas
{
    public interface IGasMovementStrategy
    {
        GasMovement GenerateGasMovement(GasUnit source, List<GasUnit> neighbors);
    }
}