namespace ssl.Role
{
    /// <summary>
    /// Player's role
    /// </summary>
    public abstract class Role
    {
        public Role(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}