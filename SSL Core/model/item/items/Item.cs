using SSL_Core.model.player;

namespace SSL_Core.model.item.items
{
    public abstract class Item
    {
        public string Id { get; } // APPLE
        public string Name { get; } // Apple
        public string Type { get; } // food
        public int MaxStack { get; } // 100 ?
        public bool DestroyOnUse { get; }
        
        public Item(string id, string name, string type, int maxStack = 1, bool destroyOnUse = false)
        {
            Id = id;
            Name = name;
            Type = type;
            MaxStack = maxStack;
            DestroyOnUse = destroyOnUse;
        }

        public Item(string id, string name, string type, bool destroyOnUse) : this(id, name, type, 1, destroyOnUse)
        { }

        /// <summary>
        /// Utilise l'objet
        /// TODO : implementer la déscruction à l'utilisation 
        /// </summary>
        public abstract void Use(Player player);

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}
