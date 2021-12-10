using Sandbox;
using ssl.Player;

namespace ssl.Modules.Roles.Instances;

public class Ghost : Role
{
	private const float RenderingAlpha = 0.25f;
	private const float BasicAlpha = 1f;
	
	public override void OnSpawn( SslPlayer sslPlayer )
	{
		base.OnSpawn(sslPlayer);

		sslPlayer.Transmit = TransmitType.Owner;
		Color color = sslPlayer.RenderColor;
		color.a = RenderingAlpha;
		sslPlayer.RenderColor = color;
		sslPlayer.RemoveCollisionLayer(CollisionLayer.PhysicsProp);
		sslPlayer.RemoveCollisionLayer(CollisionLayer.Player);
	}

	public override void OnUnassigned( SslPlayer sslPlayer )
	{
		base.OnUnassigned(sslPlayer);

		Color color = sslPlayer.RenderColor;
		color.a = BasicAlpha;
		sslPlayer.RenderColor = color;
	}
}
