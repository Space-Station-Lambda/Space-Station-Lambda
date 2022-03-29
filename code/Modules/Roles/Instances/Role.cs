using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Constants;
using ssl.Modules.Clothes;
using ssl.Modules.Items;
using ssl.Modules.Skills;
using ssl.Player;

namespace ssl.Modules.Roles.Instances;

/// <summary>
///     Player's role
/// </summary>
public partial class Role : BaseNetworkable
{
    [Net] public string Id { get; set; }
    [Net] public string Name { get; set; }
    [Net] public string Description { get; set; }
    [Net] public string Type { get; set; }
    [Net] public string Model { get; set; }

    /// <summary>
    ///     Skills of the role
    /// </summary>
    public Dictionary<string, Skill> Skills { get; set; } = new();

    public bool Available { get; set; } = false;

    public IEnumerable<string> Clothing { get; set; }
    public IEnumerable<string> StartingItems { get; set; }

    /// <summary>
    ///     Array of factions of the role. A role can have multiple factions.
    /// </summary>
    public virtual Faction[] Faction => new[] { Roles.Faction.Protagonists };

    /// <summary>
    ///     Trigger when the role is assigned
    /// </summary>
    public virtual void OnAssigned(SslPlayer sslPlayer) { }

    /// <summary>
    ///     Trigger when the player spawn
    /// </summary>
    /// <param name="sslPlayer"></param>
    public virtual void OnSpawn(SslPlayer sslPlayer)
    {
        ItemFactory itemFactory = ItemFactory.Instance;
        foreach (string itemId in StartingItems)
        {
            try
            {
                sslPlayer.Inventory.Add(itemFactory.Create(itemId));
            }
            catch (ArgumentException e)
            {
                Log.Error($"{itemId} can't be created because of: {e.Message}");
            }
        }

        foreach (string clothingId in Clothing)
        {
            sslPlayer.ClothesHandler.AttachClothes((ItemClothes) itemFactory.Create(clothingId));
        }
    }

    /// <summary>
    ///     Trigger when the role is unassigned
    /// </summary>
    public virtual void OnUnassigned(SslPlayer sslPlayer) { }

    /// <summary>
    ///     Trigger when a player with the role is killed
    /// </summary>
    public virtual void OnKilled(SslPlayer sslPlayer)
    {
        sslPlayer.RoleHandler.AssignRole(RoleFactory.Instance.Create(Identifiers.Roles.GHOST_ID));
        sslPlayer.Respawn(sslPlayer.Position, sslPlayer.Rotation);
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

        if (obj.GetType() != GetType()) return false;

        return Equals((Role) obj);
    }

    public override int GetHashCode()
    {
        return Id != null ? Id.GetHashCode() : 0;
    }
}