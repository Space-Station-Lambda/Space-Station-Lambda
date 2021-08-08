﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using ssl.Items.Data;
using ssl.Player.Roles;

namespace ssl.Player
{
    /// <summary>
    /// Player's role
    /// </summary>
    public abstract class Role
    {
        public static Dictionary<string, Role> All = new()
        {
            { "assistant", new Assistant() },
            { "captain", new Captain() },
            { "engineer", new Engineer() },
            { "ghost", new Ghost() },
            { "guard", new Guard() },
            { "janitor", new Janitor() },
            { "scientist", new Scientist() },
            { "traitor", new Traitor() },
        };

        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string Model => "models/units/simpleterry.vmdl";
        public virtual IEnumerable<string> Clothing => new HashSet<string>();
        public virtual IEnumerable<string> Items => new List<string>();

        /// <summary>
        /// Trigger when the role is assigned
        /// </summary>
        public virtual void OnAssigned(MainPlayer player){}

        /// <summary>
        /// Trigger when the player spawn
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnSpawn(MainPlayer player)
        {
            foreach (ItemData itemData in Items.Select(itemDataId => Gamemode.Instance.ItemRegistry.GetItemById(itemDataId)))
            {
                player.Inventory.Add(itemData.Create());
            }
        }

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