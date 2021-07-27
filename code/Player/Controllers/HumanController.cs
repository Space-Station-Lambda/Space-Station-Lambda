using System;
using Sandbox;
using Input = Sandbox.Input;

namespace ssl.Player.Controllers
{
	public partial class HumanController : BasePlayerController
	{
		private Vector3 mins;
		private Vector3 maxs;
		private Unstuck unstuck;
		
		public HumanController()
		{
			unstuck = new Unstuck(this);
		}
		
		public Vector3 GravityVector { get; set; } = new(0, 0, -981F);

		public float BodyHeight { get; set; } = 72.0F;
		public float EyeHeight { get; set; } = 64.0F;
		public float BodyGirth { get; set; } = 16.0F;

		public float CurrentSpeed => Velocity.Length;
		public float WalkSpeed { get; set; } = 150.0F;
		public float WalkAcceleration { get; set; } = 300.0F;

		public bool IsGrounded => GroundEntity != null;
		public Surface GroundSurface { get; set; }
		public float GroundAngle { get; set; } = 46.0F;


		private void Walk()
		{
			WishVelocity = new Vector3(Input.Forward, Input.Left, 0);
			WishVelocity = EyeRot * WishVelocity.ClampLength(1) * WalkSpeed;
			
			Accelerate(WalkAcceleration);
		}

		public override void Simulate()
		{
			UpdateBBox();
			
			EyePosLocal = Vector3.Up * (EyeHeight * Pawn.Scale);
			EyeRot = Input.Rotation;
			
			if (unstuck.TestAndFix())
			{
				return;
			}
			
			Gravity();
			
			UpdateGroundEntity();
			
			if (IsGrounded)
			{
				Walk();
				GroundFriction();
			}
			
			
			TryPlayerMove();
			
			DebugOverlay.ScreenText(0, $"    IsGrounded: {IsGrounded}");
			DebugOverlay.ScreenText(1,$"       Velocity: {Velocity}  ({Velocity.Length}");
			DebugOverlay.ScreenText(2,$"SurfaceFriction: {GroundSurface?.Friction}");
			DebugOverlay.ScreenText(3,$"  Ground Normal: {GroundNormal}");
		}

		private void Gravity()
		{
			if (!IsGrounded)
			{
				Velocity += GravityVector * Time.Delta;
			}
			else
			{
				Velocity = Velocity.WithZ(0);
			}
		}

		private void GroundFriction()
		{
			if (CurrentSpeed < 0.1f)
			{
				Velocity = Vector3.Zero;
				return;
			}

			if (!IsGrounded)
			{
				return;
			}
			
			// Friction = coefficient * force pushing on ground
			float frictionForce = GroundSurface.Friction * Math.Clamp(Vector3.Dot(-GroundNormal, GravityVector), 0, 1) * GravityVector.Length;
			
			DebugOverlay.ScreenText(5,$"  Friction Force: {frictionForce}");

			float newSpeed = CurrentSpeed - frictionForce * Time.Delta;
			if (newSpeed < 0)
			{
				newSpeed = 0;
			}
			
			Velocity *= newSpeed / CurrentSpeed;
		}

		/// <summary>
		/// Updates the GroundEntity property and all the other related properties like ground normal, surface, etc.
		/// </summary>
		private void UpdateGroundEntity()
		{
			Vector3 startPos = Position + Vector3.Up * 0.1f;
			Vector3 endPos = Position - Vector3.Up * 2.0f;

			TraceResult trace = TraceBBox(startPos, endPos, mins, maxs, 4.0F);

			if (trace.Hit)
			{
				GroundNormal = trace.Normal;
				GroundEntity = trace.Entity;
				GroundSurface = trace.Surface;

				BaseVelocity = GroundEntity.Velocity;
			}
			else
			{
				GroundNormal = Vector3.Zero;
				GroundEntity = null;
				GroundSurface = null;
				
				BaseVelocity = Vector3.Zero;
			}
		}

		private void Accelerate(float acceleration)
		{
			Vector3 accelerationVelocity = WishVelocity.Normal * acceleration * Time.Delta;

			Velocity += accelerationVelocity;
		}

		protected virtual void TryPlayerMove()
		{
			if (Velocity.Length <= 1.0F)
			{
				Velocity = Vector3.Zero;
				return;
			}
			MoveHelper mover = new(Position, Velocity);
			mover.Trace = mover.Trace.Size(mins, maxs).Ignore(Pawn);
			mover.MaxStandableAngle = GroundAngle;

			mover.TryMove(Time.Delta);
			
			Position = mover.Position;
			Velocity = mover.Velocity;
		}

		/// <summary>
		/// Traces the current bbox and returns the result.
		/// liftFeet will move the start position up by this amount, while keeping the top of the bbox at the same
		/// position. This is good when tracing down because you won't be tracing through the ceiling above.
		/// </summary>
		public override TraceResult TraceBBox( Vector3 start, Vector3 end, float liftFeet = 0.0f )
		{
			return TraceBBox( start, end, mins, maxs, liftFeet );
		}

		protected virtual void UpdateBBox()
		{
			mins = new Vector3( -BodyGirth, -BodyGirth, 0 ) * Pawn.Scale;
			maxs = new Vector3( +BodyGirth, +BodyGirth, BodyHeight ) * Pawn.Scale;
		}

		public override void FrameSimulate()
		{
			base.FrameSimulate();

			EyeRot = Input.Rotation;
		}
	}
}
