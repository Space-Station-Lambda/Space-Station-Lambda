using System;

namespace SSL_Core.model
{
    /**
     * Le rôle associé a un joueur
     */
    public abstract class Role
    {
        public String Name { get; }

        public Role(String name)
        {
            Name = name;
        }
    }
}