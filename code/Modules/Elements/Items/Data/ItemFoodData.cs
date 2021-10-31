using Sandbox;

namespace ssl.Modules.Elements.Items.Data
{
    [Library("food")]
    public class ItemFoodData : ItemData
    {
        public int FeedingValue { get; set; }
        public string WasteItem { get; set; }
    }
}