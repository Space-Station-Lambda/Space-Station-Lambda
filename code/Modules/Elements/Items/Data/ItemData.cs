using Sandbox;

namespace ssl.Modules.Elements.Items.Data
{
    /// <summary>
    /// Stores data to create an Item instance.
    /// </summary>
    [Library("item")]
    public class ItemData : BaseData
    {
        public string Description { get; set; } = "This is my item";
        public int HoldType { get; set; } = 1;
    }
}