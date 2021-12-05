using System;
using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Roles.Types.Others;

public class Ghost : Role
{
	private const float RenderingAlpha = 0.25f;
	private const float BasicAlpha = 1f;

	public override string Id => "ghost";
	public override string Name => "Ghost";
	public override string Description => "Ghost";
	public override string Category => "ghost";

	//TODO Implement the old role for the ghost (need his old faction)
	public override Faction[] Faction =>
		Array.Empty<Faction>(); // The ghost don't have any faction. Because you know ...

	public override IEnumerable<string> Clothing => new HashSet<string>();

	public override void OnSpawn( SslPlayer sslPlayer )
	{
		base.OnSpawn(sslPlayer);

		sslPlayer.Transmit = TransmitType.Owner;
		sslPlayer.RenderAlpha = RenderingAlpha;
		sslPlayer.RemoveCollisionLayer(CollisionLayer.PhysicsProp);
		sslPlayer.RemoveCollisionLayer(CollisionLayer.Player);
	}

	public override void OnUnassigned( SslPlayer sslPlayer )
	{
		base.OnUnassigned(sslPlayer);

		sslPlayer.RenderAlpha = BasicAlpha;
	}
}
