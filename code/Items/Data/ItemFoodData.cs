using ssl.Items.Carriables;

namespace ssl.Items.Data
{
    public class ItemFoodData : ItemData
    {
        public ItemFoodData(string id, string name, string model, int feedingValue) : base(id, name, model)
        {
            FeedingValue = feedingValue;
        }

        public int FeedingValue { get; }
        
        public override Item Create()
        {
            return new ItemFood(this);
        }
    }
}