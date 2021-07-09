namespace ssl.Systems.gas
{
    public class GasMovement
    {
        private AtmosUnit atmosUnit;
        private AtmosUnit atmosUnitSource;
        private AtmosUnit atmosUnitTarget;

        public GasMovement(AtmosUnit atmosUnitSource, AtmosUnit atmosUnitTarget, AtmosUnit atmosUnit)
        {
            this.atmosUnitSource = atmosUnitSource;
            this.atmosUnitTarget = atmosUnitTarget;
            this.atmosUnit = atmosUnit;
        }

        public void Resolve()
        {
            atmosUnitSource.Value -= atmosUnit.Value;
            atmosUnitTarget.Value += atmosUnit.Value;
        }
    }
}