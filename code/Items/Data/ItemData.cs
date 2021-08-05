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

        public abstract Item create();
        
        public virtual bool Equals(Item obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return Equals(obj.Id, Id);
        }
    }
}