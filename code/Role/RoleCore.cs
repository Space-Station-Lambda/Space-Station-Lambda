namespace ssl.Role
{
    /// <summary>
    /// Player's role
    /// </summary>
    public abstract class RoleCore
    {
        public string Name { get; }
        
        public RoleCore(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}