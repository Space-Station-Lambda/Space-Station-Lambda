using System.Collections.Generic;

namespace SSL_Core.system.gas
{
    public class GasMovementBasicStrategy : IGasMovementStrategy
    {
        public GasMovement GenerateGasMovement(GasUnit source, List<GasUnit> neighbours)
        {
            GasUnit target = source;
            foreach (GasUnit gasUnit in neighbours)
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