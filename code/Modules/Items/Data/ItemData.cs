using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Items.Data
{
    /// <summary>
    /// Stores data to create an Item instance.
    /// </summary>
    public class ItemData
    {
        public ItemData(string id, string name, string model, HoldType holdType = HoldType.None)
        {
            Id = id;
            Name = name;
            Model = model;
            HoldType = holdType;
        }

        public string Id { get; }
        public string Name { get; }
        public string Model { get; }
        public HoldType HoldType { get; }

        /// <summary>
        /// Create a new item with current data
        /// </summary>
        /// <returns>The new item</returns>
        public virtual Item Create()
        {
            return new Item(this);
        }
    }
}