using Sandbox;
using Input = Sandbox.Input;

namespace ssl.Player.Controllers
{
	[Library]
	public partial class HumanController : BasePlayerController
	{
		public HumanController()
		{
			
		}
		public float WalkSpeed { get; set; } = 150.0f;
		public float EyeHeight { get; set; } = 64.0f;

		public override void Simulate()
		{
			EyePosLocal = Vector3.Up * (EyeHeight * Pawn.Scale);
			EyeRot = Input.Rotation;

			// Walk
			WishVelocity = new Vector3(Input.Forward, Input.Left, 0);
			Velocity = EyeRot * WishVelocity.ClampLength(1) * WalkSpeed;
			
			TryPlayerMove();
			
			DebugOverlay.ScreenText( 0, $"    WishVelocity: {WishVelocity}" );
			DebugOverlay.ScreenText( 1, $"        Velocity: {Velocity}" );
			DebugOverlay.ScreenText( 2, $"         Eye Rot: {EyeRot}" );
		}

		public override void FrameSimulate()
		{
			base.FrameSimulate();

			EyeRot = Input.Rotation;
		}
		
		public virtual void TryPlayerMove()
		{
			MoveHelper mover = new MoveHelper(Position, Velocity);
			mover.Trace = mover.Trace.Size(0, 0).Ignore(Pawn);

			mover.TryMove(Time.Delta);

			Position = mover.Position;
			Velocity = mover.Velocity;
		}
	}
}
