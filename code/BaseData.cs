using Sandbox;

namespace ssl
{
    [Library("base")]
    public class BaseData : Asset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
    }
}