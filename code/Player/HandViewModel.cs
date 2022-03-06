using System;
using Sandbox;

namespace ssl.Player;

/// <summary>
///     View Model allowing us to mirror Terry's hands in first person to show his holding items.
/// </summary>
public class HandViewModel : AnimEntity
{
    private const string VIEW_MODEL_PATH = "models/players/citizen/first_person_arms.vmdl";
    
    public HandViewModel()
    {
        Host.AssertClient();

        SetModel(VIEW_MODEL_PATH);
        RemoveHoldingEntity();
    }

    public AnimEntity HoldingEntity { get; private set; }


    /// <summary>
    ///     Modify the holdtype of the animgraph and the offset of the viewmodel?
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetHoldType(HoldType holdType)
    {
        EnableDrawing = true;
        switch (holdType)
        {
            case HoldType.None:
                EnableDrawing = false;
                break;
            case HoldType.Pistol:
                SetAnimParameter("FingerAdjustment_BlendNeutralPose_R", 0);
                break;
            case HoldType.Hand:
                SetAnimParameter("FingerAdjustment_BlendNeutralPose_R", 1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(holdType), holdType, null);
        }
    }

    public override void PostCameraSetup(ref CameraSetup camSetup)
    {
        Position = camSetup.Position;
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
        HoldingEntity = new AnimEntity { 
            UsePhysicsCollision = false, 
            EnableViewmodelRendering = true, 
            Owner = this,
            Model = entity.Model
        };
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