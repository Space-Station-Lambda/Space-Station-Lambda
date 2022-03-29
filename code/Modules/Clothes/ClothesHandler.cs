using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Clothes;

/// <summary>
///     Manage the clothing system for an entity
/// </summary>
public class ClothesHandler : EntityComponent<SslPlayer>
{
    private readonly Dictionary<ClothesSlot, ItemClothes> clothes = new();

    /// <summary>
    ///     Attach a piece of clothes to the entity
    /// </summary>
    public void AttachClothes(ItemClothes clothing)
    {
        clothing.SetParent(Entity, true);

        throw new NotImplementedException();
    }

    /// <summary>
    ///     Remove all clothes of the entity
    /// </summary>
    public void Strip()
    {
        foreach ((ClothesSlot _, ItemClothes clothing) in clothes)
        {
            clothing.Parent = null;
        }

        clothes.Clear();
    }
}