using System;
using Sandbox;

namespace ssl.Player;

/// <summary>
///     View Model allowing us to mirror Terry's hands in first person to show his holding items.
/// </summary>
public class HandViewModel : AnimEntity
{
    private const string VIEW_MODEL_PATH = "models/players/citizen/v_citizen.vmdl";
    private const string ANIM_KEY_HOLD_TYPE = "holdtype";
    private const string ANIM_KEY_BODY_WEIGHT = "aim_body_weight";
    private const string BODY_GROUP_HEAD = "head";
    private const string BODY_GROUP_LEGS = "legs";
    private const string BODY_GROUP_FEET = "feet";

    private Vector3 offset;

    public HandViewModel()
    {
        Host.AssertClient();

        SetModel(VIEW_MODEL_PATH);
        SetBodyGroup(BODY_GROUP_HEAD, 1);
        SetBodyGroup(BODY_GROUP_LEGS, 1);
        SetBodyGroup(BODY_GROUP_FEET, 1);
        SetAnimParameter(ANIM_KEY_BODY_WEIGHT, 0f);
        RemoveHoldingEntity();
    }

    public AnimEntity HoldingEntity { get; private set; }

    private static Vector3 HandOffset { get; } = -(Vector3.Up * 50 + Vector3.Right);
    private static Vector3 PistolOffset { get; } = -(Vector3.Up * 60 + Vector3.Right);

    /// <summary>
    ///     Modify the holdtype of the animgraph and the offset of the viewmodel?
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetHoldType(HoldType holdType)
    {
        SetAnimParameter(ANIM_KEY_HOLD_TYPE, (int) holdType);
        offset = holdType switch
        {
            HoldType.None => offset,
            HoldType.Pistol => PistolOffset,
            HoldType.Hand => HandOffset,
            _ => throw new ArgumentOutOfRangeException(nameof(holdType), holdType, null)
        };
    }

    public override void PostCameraSetup(ref CameraSetup camSetup)
    {
        Position = camSetup.Position + offset * Input.Rotation;
        Rotation = camSetup.Rotation;
    }

    /// <summary>
    ///     Sets the entity that is in the player's hand model
    /// </summary>
    /// <param name="entity"></param>
    public void SetHoldingEntity(AnimEntity entity)
    {
        Host.AssertClient();
        HoldingEntity?.Delete();
        HoldingEntity = new AnimEntity { UsePhysicsCollision = false, EnableViewmodelRendering = true, Owner = this };
        HoldingEntity.Model = entity.Model;
        HoldingEntity.SetParent(this, true);
    }

    /// <summary>
    ///     Remove the entity in the hand
    /// </summary>
    public void RemoveHoldingEntity()
    {
        HoldingEntity?.Delete();
        HoldingEntity = null;
    }
}