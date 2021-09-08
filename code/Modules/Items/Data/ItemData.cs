using Sandbox;

namespace ssl.Modules.Items.Data
{
    /// <summary>
    /// Stores data to create an Item instance.
    /// </summary>
    public class ItemData : BaseData
    {
        public string Description { get; set; } = "This is my item";
        public int HoldType { get; set; } = 1;
    }
}