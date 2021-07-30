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
        public virtual void OnAssigned(MainPlayer player){}
        /// <summary>
        /// Trigger when the player spawn
        /// </summary>
        public virtual void OnSpawn(MainPlayer player){}
        /// <summary>
        /// Trigger when the role is unassigned
        /// </summary>
        public virtual void OnUnassigned(MainPlayer player){}
        /// <summary>
        /// Trigger when a player with the role is killed
        /// </summary>
        public virtual void OnKilled(MainPlayer player)
        {
            player.RoleHandler.AssignRole(new Ghost());
            player.Respawn(player.Position, player.Rotation);
        }
        public override string ToString()
        {
            return $"[{Id}]{Name}";
        }
    }
}