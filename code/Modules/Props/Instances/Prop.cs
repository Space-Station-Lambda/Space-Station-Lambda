using Sandbox;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Instances;

/// <summary>
///     A prop is an object not in inventory
///     Inspired by sandbox Props
/// </summary>
public class Prop : WorldEntity, ISelectable
{
	
	public string Id { get; set; }
	public string Model { get; set; }
	
	public virtual void OnSelectStart( SslPlayer sslPlayer )
	{
	}

	public virtual void OnSelectStop( SslPlayer sslPlayer )
	{
	}

	public virtual void OnSelect( SslPlayer sslPlayer )
	{
	}

	public virtual void OnInteract( SslPlayer sslPlayer, int strength )
	{
	}

	public override void Spawn()
	{
		base.Spawn();

		SetupPhysicsFromModel(PhysicsMotionType.Static);
		PhysicsEnabled = false;
		CollisionGroup = CollisionGroup.Interactive;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
	}
}
