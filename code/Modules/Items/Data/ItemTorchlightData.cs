using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items.Data
{
    public class ItemTorchlightData : ItemData
    {
        public ItemTorchlightData(string id, string name, string model) : base(id, name, model)
        {
        }

        public override ItemTorchlight Create()
        {
            return new ItemTorchlight(this);
        }
    }
}