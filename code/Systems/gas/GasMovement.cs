namespace ssl.Systems.gas
{
    public class GasMovement
    {
        private GasUnit gasUnit;
        private GasUnit source;
        private GasUnit target;

        public GasMovement(GasUnit source, GasUnit target, GasUnit gasUnit)
        {
            this.source = source;
            this.target = target;
            this.gasUnit = gasUnit;
        }

        public void resolve()
        {
            source.Value -= gasUnit.Value;
            target.Value += gasUnit.Value;
        }
    }
}