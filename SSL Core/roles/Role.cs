namespace SSL_Core.roles
{
    /// <summary>
    /// Role du joueur
    /// </summary>
    public abstract class Role
    {
        public string Name { get; }

        public Role(string name)
        {
            Name = name;
        }
    }
}