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
        public abstract HashSet<string> Clothing { get; }
        /// <summary>
        /// Trigger when the role is assigned
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnAssigned(MainPlayer player){}
        /// <summary>
        /// Trigger when the player spawn
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnSpawn(MainPlayer player){}
        /// <summary>
        /// Trigger when the role is unasigned
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnUnassigned(MainPlayer player){}
        public override string ToString()
        {
            return $"[{Id}]{Name}";
        }
    }
}