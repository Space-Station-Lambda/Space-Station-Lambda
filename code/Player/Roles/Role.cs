using System.Collections.Generic;

namespace ssl.Player.Roles
{
    /// <summary>
    /// Player's role
    /// </summary>
    public abstract class Role
    {
        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string Model => "models/units/simpleterry.vmdl";
        public virtual IEnumerable<string> Clothing => new HashSet<string>();
        public virtual Dictionary<string, int> Items => new();
        /// <summary>
        /// Trigger when the role is assigned
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnAssigned(MainPlayer player){}

        /// <summary>
        /// Trigger when the player spawn
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnSpawn(MainPlayer player)
        {
            foreach ((string id, int amount) in Items)
            {
                player.Inventory.AddItem(id, amount);
            }
        }
        /// <summary>
        /// Trigger when the role is unasigned
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnUnassigned(MainPlayer player){}
        public override string ToString()
        {
            return $"[{Id}]{Name}";
        }

        protected bool Equals(Role other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Role)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}