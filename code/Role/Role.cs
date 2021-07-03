namespace SSL.Role
{
    /// <summary>
    /// Player's role
    /// </summary>
    public abstract class Role
    {
        public string Name { get; }

        public Role(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}