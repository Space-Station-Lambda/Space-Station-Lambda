using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items.Data
{
    public class ItemMopData : ItemData
    {
        public ItemMopData(string id, string name, string model) : base(id, name, model)
        {
        }
        
        public override ItemMop Create()
        {
            return new ItemMop(this);
        }
    }
}