using Sandbox;
using Input = Sandbox.Input;

namespace ssl.Player.Controllers
{
	[Library]
	public partial class HumanController : BasePlayerController
	{
		private Angles lookAngles = Angles.Zero;
		
		public HumanController()
		{
			
		}

		public Vector2 ClampedPitch { get; set; } = new(-89, 89);
		public float EyeHeight { get; set; } = 64.0f;
		public bool IsPitchClamped { get; set; } = true;

		public override void Simulate()
		{
			EyePosLocal = Vector3.Up * (EyeHeight * Pawn.Scale);
			EyeRot = Rotation.From(lookAngles);
		}

		public override void FrameSimulate()
		{
			base.FrameSimulate();

			EyeRot = Rotation.From(lookAngles);
		}

		public override void BuildInput(InputBuilder input)
		{
			// We get the raw input so we're not clamped by default
			lookAngles += input.AnalogLook;
			lookAngles = lookAngles.WithRoll(0);
			
			if (IsPitchClamped)
			{
				lookAngles.pitch = lookAngles.pitch.Clamp(ClampedPitch.x, ClampedPitch.y);
			}
			
			input.Clear();
			input.StopProcessing = true;

			base.BuildInput(input);
		}
	}
}
