using System.Collections.Generic;

namespace SSL_Core.system.gas
{
    public class GasMovementBasicStrategy : IGasMovementStrategy
    {
        public GasMovement GenerateGasMovement(GasUnit source, List<GasUnit> neighbors)
        {
            GasUnit target = source;
            foreach (GasUnit gasUnit in neighbors)
            {
                if (gasUnit.Value < target.Value)
                {
                    target = gasUnit;
                }
            }

            GasUnit gasUnitToSend = new GasUnit();
            gasUnitToSend.Value = 1;
            return new GasMovement(source, target, gasUnitToSend);
        }
    }
}