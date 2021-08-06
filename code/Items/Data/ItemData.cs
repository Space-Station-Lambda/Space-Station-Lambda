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

        public virtual Carriables.Item Create()
        {
            return new(this);
        }
    }
}