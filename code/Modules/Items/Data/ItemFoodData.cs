using Sandbox;

namespace ssl.Modules.Items.Data
{
    [Library("food")]
    public class ItemFoodData : ItemData
    {
        public int FeedingValue { get; set; }
        public string WasteItem { get; set; }
    }
}