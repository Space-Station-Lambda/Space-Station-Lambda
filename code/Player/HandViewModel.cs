using System;
using Sandbox;

namespace ssl.Player
{
    /// <summary>
    /// View Model allowing us to mirror Terry's hands in first person to show his holding items.
    /// </summary>
    public class HandViewModel : AnimEntity
    {
        private const string ViewModelPath = "models/players/citizen/v_citizen.vmdl";
        private const string AnimKeyHoldType = "holdtype";
        private const string AnimKeyBodyWeight = "aim_body_weight";
        private const string BodyGroupHead = "head";
        private const string BodyGroupLegs = "legs";
        private const string BodyGroupFeet = "feet";
        private AnimEntity holdingEntity;

        private Vector3 offset;

        public HandViewModel()
        {
            Host.AssertClient();
            
            SetModel(ViewModelPath);
            SetBodyGroup(BodyGroupHead, 1);
            SetBodyGroup(BodyGroupLegs, 1);
            SetBodyGroup(BodyGroupFeet, 1);
            SetAnimFloat(AnimKeyBodyWeight, 0f);
            RemoveHoldingEntity();
        }

        private static Vector3 NoneOffset { get; } = -(Vector3.Up * 50 + Vector3.Right);
        private static Vector3 HandOffset { get; } = -(Vector3.Up * 50 + Vector3.Right);
        private static Vector3 PistolOffset { get; } = -(Vector3.Up * 60 + Vector3.Right);

        /// <summary>
        /// Modify the holdtype of the animgraph and the offset of the viewmodel?
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetHoldType(HoldType holdType)
        {
            SetAnimInt(AnimKeyHoldType, (int)holdType);
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
        /// Sets the entity that is in the player's hand model
        /// </summary>
        /// <param name="entity"></param>
        public void SetHoldingEntity(AnimEntity entity)
        {
            holdingEntity?.Delete();
            holdingEntity = new AnimEntity
            {
                UsePhysicsCollision = false,
                EnableViewmodelRendering = true,
                Owner = this
            };
            holdingEntity.SetModel(entity.GetModel());
            holdingEntity.SetParent(this, true);
        }

        /// <summary>
        /// Remove the entity in the hand
        /// </summary>
        public void RemoveHoldingEntity()
        {
            holdingEntity?.Delete();
            holdingEntity = null;
        }
    }
}