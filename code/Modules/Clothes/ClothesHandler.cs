using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Clothes;

/// <summary>
///     Manage the clothing system for an entity
/// </summary>
public class ClothesHandler : EntityComponent<SslPlayer>
{
    private readonly Dictionary<ClothesSlot, ItemClothes> clothes = new();

    protected override void OnActivate()
    {
        base.OnActivate();
        clothes.TryAdd(ClothesSlot.Hat, null);
        clothes.TryAdd(ClothesSlot.Shirt, null);
        clothes.TryAdd(ClothesSlot.Trousers, null);
        clothes.TryAdd(ClothesSlot.Shoes, null);
    }

    /// <summary>
    ///     Attach a piece of clothes to the entity
    /// </summary>
    public void AttachClothes(ItemClothes clothing)
    {
        if (clothes[clothing.Slot] != null) return;
        clothing.SetParent(Entity, true);
        clothes[clothing.Slot] = clothing;
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