using System;
using System.Runtime.Serialization;

namespace Ssl.Item.items
{
    [DataContract(Name = "Item",  Namespace = "items")]
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
        
        public String Type { get; private set; }

        protected Item()
        {
        }
        
        public Item(string id, string name, string type = "",int maxStack = 1, bool destroyOnUse = false)
        {
            Id = id;
            Name = name;
            Type = type;
            MaxStack = maxStack;
            DestroyOnUse = destroyOnUse;
        }

        public Item(string id, string name, bool destroyOnUse) : this(id, name, "", 1, destroyOnUse)
        { }

        /// <summary>
        /// Uses the object
        /// TODO : implement the destroy on use
        /// </summary>
        public abstract void Use(Player.Player player);

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}
