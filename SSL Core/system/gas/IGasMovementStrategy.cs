using System.Collections.Generic;

namespace SSL_Core.system.gas
{
    public interface IGasMovementStrategy
    {
        GasMovement GenerateGasMovement(GasUnit source, List<GasUnit> neighbors);
    }
}