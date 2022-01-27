using System.Collections.Generic;
using Sandbox;

namespace ssl.Modules.Items.Instances;

public abstract class Carriable : WorldEntity
{
    private const string DEFAULT_ATTACHMENT_OR_BONE = "hold_R";

    public override void Spawn()
    {
        SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
        MoveType = MoveType.Physics;
        CollisionGroup = CollisionGroup.Weapon;
        SetInteractsAs(CollisionLayer.Hitbox);
        PhysicsEnabled = true;
        UsePhysicsCollision = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;
    }

    public override bool CanCarry(Entity carrier)
    {
        return true;
    }

    public override void OnCarryStart(Entity carrier)
    {
        if (CanBeBoneMerged(carrier))
            SetParent(carrier, true);
        else
            SetParent(carrier, DEFAULT_ATTACHMENT_OR_BONE, Transform.Zero);

        Owner = carrier;
        MoveType = MoveType.None;
        EnableDrawing = false;
        EnableAllCollisions = false;
    }

    public override void OnCarryDrop(Entity dropper)
    {
        SetParent(null);
        Owner = null;
        MoveType = MoveType.Physics;
        EnableDrawing = true;
        EnableAllCollisions = true;
    }

    /// <summary>
    ///     This entity has become the active entity. This most likely
    ///     means a player was carrying it in their inventory and now
    ///     has it in their hands.
    /// </summary>
    public override void ActiveStart(Entity ent)
    {
        base.ActiveStart(ent);

        EnableDrawing = true;
    }

    /// <summary>
    ///     This entity has stopped being the active entity. This most
    ///     likely means a player was holding it but has switched away
    ///     or dropped it (in which case dropped = true)
    /// </summary>
    public override void ActiveEnd(Entity ent, bool dropped)
    {
        base.ActiveEnd(ent, dropped);

        // If we're just holstering, then hide us
        if (!dropped) EnableDrawing = false;
    }

    /// <summary>
    ///     Checks if we can bone merge this carriable into parent's model.
    /// </summary>
    private bool CanBeBoneMerged(Entity ent)
    {
        Model model = Model;
        if (model.IsError || ent is not ModelEntity entity) return false;

        Model entityModel = entity.Model;

        // List all bone names from parent entity
        List<string> entityBoneNames = new();
        for (int parentIndex = 0; parentIndex < entityModel.BoneCount; parentIndex++)
        {
            entityBoneNames.Add(entityModel.GetBoneName(parentIndex));
        }

        // Checks if any bone in carriable entity is also in parent
        for (int carriableIndex = 0; carriableIndex < model.BoneCount; carriableIndex++)
        {
            string boneName = model.GetBoneName(carriableIndex);
            if (entityBoneNames.Contains(boneName)) return true;
        }

        return false;
    }
}