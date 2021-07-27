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

		private const float BodyHeight  = 72.0F;
	    private const float EyeHeight  = 64.0F;
		private const float BodyGirth = 16.0F;
		
		private const float StopSpeed = 100.0f;
		private const float WalkAcceleration = 800.0f;
		private const float WalkSpeed = 150.0f;
		
		private const float GroundAngle = 46.0f;
		
		public HumanController()
		{
			unstuck = new Unstuck(this);

		}
		public Vector3 GravityVector { get; set; } = new(0, 0, -981F);
		public float CurrentSpeed => Velocity.Length;
		
		public bool IsGrounded => GroundEntity != null;
		public Surface GroundSurface { get; set; }
		public float SurfaceFriction { get; set; } = 4;
		
		
		public override void Simulate()
		{
			UpdateBBox();

			EyePosLocal = Vector3.Up * (EyeHeight * Pawn.Scale);
			EyeRot = Input.Rotation;
			
			//If the player is stuck, fix and stop
			if (unstuck.TestAndFix()) return;

			ApplyGravity();
			
			UpdateGroundEntity();
			
			if (IsGrounded)
			{
				Walk();
				ApplyFriction(GroundSurface.Friction * SurfaceFriction);
			}
			
			DebugOverlay.ScreenText(0, $"    IsGrounded: {IsGrounded}");
			DebugOverlay.ScreenText(1,$"       Velocity: {Velocity}  ({Velocity.Length})");
			DebugOverlay.ScreenText(2,$"SurfaceFriction: {GroundSurface?.Friction}");
			DebugOverlay.ScreenText(3,$"  Ground Normal: {GroundNormal}");

			TryPlayerMove();
		}
		
		private void Walk()
		{
			WishVelocity = new Vector3(Input.Forward, Input.Left, 0);
			WishVelocity = EyeRot * WishVelocity.ClampLength(1);
			WishVelocity *= WalkAcceleration;

			if (!Velocity.IsNearZeroLength)
			{
				Vector3 projectedVelocity = Velocity.Dot(WishVelocity) / Velocity.Length * Velocity.Normal;
				Vector3 rejectedVelocity = WishVelocity - projectedVelocity;
				
				if (CurrentSpeed + projectedVelocity.Length * Time.Delta > WalkSpeed && projectedVelocity.Length > 0f)
				{
					projectedVelocity *= ((WalkSpeed - CurrentSpeed) / projectedVelocity.Length).Clamp(0f, 1f);
				}

				WishVelocity = projectedVelocity + rejectedVelocity;
			}
			
			Accelerate();
		}


		private void ApplyGravity()
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
		
		/// <summary>
		/// Apply a specific amount of friction
		/// </summary>
		/// <param name="frictionAmount">Friction to apply</param>
		private void ApplyFriction( float frictionAmount = 1.0f )
		{
			if (CurrentSpeed < 0.1f) return;
			
			float usedSpeed = CurrentSpeed < StopSpeed ? StopSpeed : CurrentSpeed;
			float droppedSpeed = usedSpeed * Time.Delta * frictionAmount;
			float newSpeed = CurrentSpeed - droppedSpeed;
			
			if (newSpeed < 0) newSpeed = 0;
			
			newSpeed /= CurrentSpeed;
			Velocity *= newSpeed;
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

		private void Accelerate()
		{
			Vector3 acceleration = WishVelocity * Time.Delta;

			Velocity += acceleration;
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
		/// <summary>
		/// BoundingBox (collision box)
		/// </summary>
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
