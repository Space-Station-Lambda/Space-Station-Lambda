using System.Collections.Generic;
using System.Linq;

namespace ssl.Systems.Atmosphere
{
    public class GasMovementBasicStrategy : IGasMovementStrategy
    {
        public GasMovement GenerateGasMovement(AtmosUnit source, IEnumerable<AtmosUnit> neighbors)
        {
            AtmosUnit target = source;
            foreach (AtmosUnit gasUnit in neighbors.Where(gasUnit => gasUnit.Value < target.Value))
            {
                target = gasUnit;
            }

            AtmosUnit atmosUnitToSend = new AtmosUnit();
            atmosUnitToSend.Value = 1;
            return new GasMovement(source, target, atmosUnitToSend);
        }
    }
}