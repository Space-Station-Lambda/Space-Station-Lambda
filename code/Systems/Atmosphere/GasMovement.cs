namespace ssl.Systems.Atmosphere
{
    public class GasMovement
    {
        private readonly AtmosphericUnit atmosphericUnit;
        private readonly AtmosphericUnit atmosphericUnitSource;
        private readonly AtmosphericUnit atmosphericUnitTarget;

        public GasMovement(AtmosphericUnit atmosphericUnitSource, AtmosphericUnit atmosphericUnitTarget, AtmosphericUnit atmosphericUnit)
        {
            this.atmosphericUnitSource = atmosphericUnitSource;
            this.atmosphericUnitTarget = atmosphericUnitTarget;
            this.atmosphericUnit = atmosphericUnit;
        }

        public void Resolve()
        {
            atmosphericUnitSource.Value -= atmosphericUnit.Value;
            atmosphericUnitTarget.Value += atmosphericUnit.Value;
        }
    }
}