using ssl.Player;

namespace ssl.Modules.Statuses.Types;

public class Sickness : Status
{
	private const byte TARGET_R_COLOR = 140;
	private const byte TARGET_G_COLOR = 207;
	private const byte TARGET_B_COLOR = 100;


	public Sickness( int duration ) : base(duration)
	{
	}

	public override string Id => "status.sickness";
	public override string Name => "Sickness";
	public override string Description => "Sickness";
	public override bool IsInfinite => false;

	public override void OnTick( SslPlayer sslPlayer )
	{
		base.OnTick(sslPlayer);
		float sicknessRatio = (InitialTime - TimeLeft) / InitialTime;
		byte r = (byte)(255 - (sicknessRatio * 255 - TARGET_R_COLOR));
		byte g = (byte)(255 - (sicknessRatio * 255 - TARGET_G_COLOR));
		byte b = (byte)(255 - (sicknessRatio * 255 - TARGET_B_COLOR));
		sslPlayer.RenderColor = new Color(r, g, b);
	}

	public override void OnResolve( SslPlayer sslPlayer )
	{
		sslPlayer.RenderColor = Color.White;
		base.OnResolve(sslPlayer);
	}
}
