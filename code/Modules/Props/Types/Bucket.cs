using ssl.Modules.Props.Data;

namespace ssl.Modules.Props.Types
{
    public class Bucket : Prop<ContainerData>
    {
        public Liquid ContainedLiquid;
        
        public Bucket()
        {
        }

        public Bucket(ContainerData propData) : base(propData)
        {
            ContainedLiquid = new Liquid(propData.Capacity);
        }
    }
}