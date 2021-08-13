using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items.Data
{
    public class ItemFoodData : ItemData
    {
        public ItemFoodData(string id, string name, string model, int feedingValue) : base(id, name, model)
        {
            FeedingValue = feedingValue;
        }

        public int FeedingValue { get; }

        public override ItemFood Create()
        {
            return new(this);
        }
    }
}