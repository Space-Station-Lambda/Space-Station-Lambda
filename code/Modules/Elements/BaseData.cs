using Sandbox;

namespace ssl.Modules.Elements
{
    [Library("base")]
    public class BaseData : Asset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; } = "none";

        public override string ToString()
        {
	        return $"[{Id}] {Name}";
        }
    }
}
