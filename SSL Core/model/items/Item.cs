using System;

namespace SSL_Core.model.items
{
    public abstract class Item
    {
        public string Id { get; } // APPLE
        public string Name { get; } // Apple

        public int MaxStack { get; } // 100 ?
        
        public Item(string id, string name, int maxStack = 1)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
        }

        /// <summary>
        /// Utilise l'objet 
        /// </summary>
        public abstract void Use();
    }
}
