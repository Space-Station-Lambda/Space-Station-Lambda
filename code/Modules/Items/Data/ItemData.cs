namespace ssl.Modules.Items.Data
{
    /// <summary>
    /// Stores data to create an Item instance.
    /// </summary>
    public class ItemData : Asset
    {
        public string Id { get; set; } = "Item Id";
        public string Name { get; set; } = "Item Name";
        public string Description { get; set; } = "This is my item";
        public string Model { get; set; } = "";
        public int HoldType { get; set; } = 1;
    }
}