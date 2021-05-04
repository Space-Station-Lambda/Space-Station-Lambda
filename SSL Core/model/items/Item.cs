using System.Runtime.Serialization;
using SSL_Core.model.player;

namespace SSL_Core.model.items
{
    [DataContract(Name = "Item")]
    public abstract class Item
    {
        [DataMember]
        public string Id { get; private set; } // APPLE
        [DataMember]
        public string Name { get; private set; } // Apple
        [DataMember]
        public int MaxStack { get; private set; } // 100 ?
        [DataMember]
        public bool DestroyOnUse { get; private set; }

        protected Item()
        {
            
        }
        
        public Item(string id, string name, int maxStack = 1, bool destroyOnUse = false)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
            DestroyOnUse = destroyOnUse;
        }

        public Item(string id, string name, bool destroyOnUse) : this(id, name, 1, destroyOnUse)
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
