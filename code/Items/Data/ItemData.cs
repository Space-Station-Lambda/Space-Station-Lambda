namespace ssl.Items.Data
{
    public abstract class ItemData
    {
        public ItemData(string id, string name, string model)
        {
            Id = id;
            Name = name;
            Model = model;
        }
        
        public string Id { get; }
        public string Name { get; }
        public string Model { get; } = ""; //Find default model

        public abstract Item Create();
        
    }
}