namespace ssl.Items.Data
{
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

        public virtual Item Create()
        {
            return new(this);
        }
    }
}