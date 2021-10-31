using System;
using Sandbox;
using ssl.Modules.Elements.Items.Carriables;
using Input = Sandbox.Input;

namespace ssl.Player.Animators
{
	public class HumanAnimator : PawnAnimator
	{
		private const float TurnSpeed = 0.01F;
		private const float MaxTimeSinceFootShuffle = 0.1F;
		private const float YawDiffNoItem = 90;
		private const float YawDiffItem = 50;
		
		private const string NoClipTag = "noclip";
		private const string GroundedKey = "b_grounded";
		private const string NoClipKey = "b_noclip";
		private const string AimEyesKey = "aim_eyes";
		private const string AimHeadKey = "aim_head";
		private const string AimBodyKey = "aim_body";
		private const string AimBodyWeightKey = "aim_body_weight";
		private const string HoldTypeKey = "holdtype";
		private const string ShuffleKey = "b_shuffle";
		private const string JumpKey = "b_jump";
		
		private const string JumpEventName = "jump";

		private const string MoveDirectionKey = "move_direction";
		private const string MoveSpeedKey = "move_speed";
		private const string MoveGroundSpeedKey = "move_groundspeed";
		private const string MoveXKey = "move_x";
		private const string MoveYKey = "move_y";
		private const string MoveZKey = "move_z";

		private const string WishDirectionKey = "wish_direction";
		private const string WishSpeedKey = "wish_speed";
		private const string WishGroundSpeedKey = "wish_groundspeed";
		private const string WishXKey = "wish_x";
		private const string WishYKey = "wish_y";
		private const string WishZKey = "wish_z";

		private TimeSince timeSinceFootShuffle = 60F;

		private Vector3 AimPos => Pawn.EyePos + Input.Rotation.Forward * 200F;

		public override void Simulate()
		{
			Rotation idealRotation = Rotation.LookAt(Input.Rotation.Forward.WithZ(0), Vector3.Up);

			DoRotation(idealRotation);
			DoWalk();

			// Let the animation graph know some shit
			bool isNoClip = HasTag(NoClipTag);
			bool isGrounded = GroundEntity != null || isNoClip;
			
			SetParam(GroundedKey, isGrounded);
			SetParam(NoClipKey, isNoClip);

			// Look in the direction what the player's input is facing
			SetLookAt();

			if (Pawn.ActiveChild is Item carry)
			{
				carry.SimulateAnimator(this);
			}
			else
			{
				SetParam(HoldTypeKey, (int) HoldType.None);
				SetParam(AimBodyWeightKey, 0.5f);
			}
		}

		protected virtual void DoRotation(Rotation idealRotation)
		{
			// Our ideal player model rotation is the way we're facing
			float allowYawDiff = Pawn.ActiveChild == null ? YawDiffNoItem : YawDiffItem;

			// If we're moving, rotate to our ideal rotation
			Rotation = Rotation.Slerp(Rotation, idealRotation, WishVelocity.Length * Time.Delta * TurnSpeed);

			// Clamp the foot rotation to within 120 degrees of the ideal rotation
			Rotation = Rotation.Clamp(idealRotation, allowYawDiff, out float change);

			// If we did restrict, and are standing still, add a foot shuffle
			if (change > 1 && WishVelocity.Length <= 1) timeSinceFootShuffle = 0;

			SetParam(ShuffleKey, timeSinceFootShuffle < MaxTimeSinceFootShuffle);
		}

		private void SetLookAt()
		{
			SetLookAt(AimEyesKey, AimPos);
			SetLookAt(AimHeadKey, AimPos);
			SetLookAt(AimBodyKey, AimPos);
		}

		private void DoWalk()
		{
			// Move Speed
			{
				Vector3 dir = Velocity;
				float forward = Rotation.Forward.Dot(dir);
				float sideward = Rotation.Right.Dot(dir);

				float angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();

				SetParam(MoveDirectionKey, angle);
				SetParam(MoveSpeedKey, Velocity.Length);
				SetParam(MoveGroundSpeedKey, Velocity.WithZ(0).Length);
				SetParam(MoveXKey, forward);
				SetParam(MoveYKey, sideward);
				SetParam(MoveZKey, Velocity.z);
			}

			// Wish Speed
			{
				Vector3 dir = WishVelocity;
				float forward = Rotation.Forward.Dot(dir);
				float sideward = Rotation.Right.Dot(dir);

				float angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();

				SetParam(WishDirectionKey, angle);
				SetParam(WishSpeedKey, WishVelocity.Length);
				SetParam(WishGroundSpeedKey, WishVelocity.WithZ(0).Length);
				SetParam(WishXKey, forward);
				SetParam(WishYKey, sideward);
				SetParam(WishZKey, WishVelocity.z);
			}
		}

		public override void OnEvent(string name)
		{
			if (name == JumpEventName)
			{
				Trigger(JumpKey);
			}

			base.OnEvent(name);
		}
	}
}
