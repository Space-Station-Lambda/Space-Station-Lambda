using ssl.Items.Carriables;

namespace ssl.Items.Data
{
    /// <summary>
    /// Stores data to create an Item instance.
    /// </summary>
    public class ItemData
    {
        public ItemData(string id, string name, string model)
        {
            Id = id;
            Name = name;
            Model = model;
        }
        
        public string Id { get; }
        public string Name { get; }
        public string Model { get; }

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