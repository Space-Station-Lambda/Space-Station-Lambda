using System.Collections.Generic;
using System.Linq;

namespace ssl.Systems.Atmosphere
{
    public class GasMovementBasicStrategy : IGasMovementStrategy
    {
        public GasMovement GenerateGasMovement(AtmosphericUnit source, IEnumerable<AtmosphericUnit> neighbors)
        {
            AtmosphericUnit target = source;
            foreach (AtmosphericUnit gasUnit in neighbors.Where(gasUnit => gasUnit.Value < target.Value))
            {
                target = gasUnit;
            }

            AtmosphericUnit atmosphericUnitToSend = new()
            {
                Value = 1
            };
            return new GasMovement(source, target, atmosphericUnitToSend);
        }
    }
}