using ssl.Modules.Props.Data;

namespace ssl.Modules.Props.Types
{
    public class TrashBin : Prop
    {
        private const int MaxTrash = 50;
        
        public TrashBin()
        {
        }

        public TrashBin(PropData propData) : base(propData)
        {
        }
    }
}