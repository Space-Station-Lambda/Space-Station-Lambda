using Sandbox;

namespace ssl.Modules.Elements.Items.Carriables
{
    public class Carriable : WorldEntity
    {
	    public Carriable()
	    {
	    }
	    
	    public Carriable(BaseData data) : base(data)
	    {
		    SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
		    MoveType = MoveType.Physics;
		    CollisionGroup = CollisionGroup.Weapon;
		    SetInteractsAs(CollisionLayer.Hitbox);
		    PhysicsEnabled = true;
		    UsePhysicsCollision = true;
		    EnableHideInFirstPerson = true;
		    EnableShadowInFirstPerson = true;
	    }

		public override bool CanCarry(Entity carrier)
		{
			return true;
		}

		public override void OnCarryStart(Entity carrier)
		{
			SetParent(carrier, true);
			Owner = carrier;
			MoveType = MoveType.None;
			EnableDrawing = false;
		}
		
		public override void OnCarryDrop(Entity dropper)
		{
			SetParent(null);
			Owner = null;
			MoveType = MoveType.Physics;
			EnableDrawing = true;
		}

		/// <summary>
		/// This entity has become the active entity. This most likely
		/// means a player was carrying it in their inventory and now
		/// has it in their hands.
		/// </summary>
		public override void ActiveStart(Entity ent)
		{
			base.ActiveStart(ent);

			EnableDrawing = true;
		}

		/// <summary>
		/// This entity has stopped being the active entity. This most
		/// likely means a player was holding it but has switched away
		/// or dropped it (in which case dropped = true)
		/// </summary>
		public override void ActiveEnd(Entity ent, bool dropped)
		{
			base.ActiveEnd(ent, dropped);
			
			// If we're just holstering, then hide us
			if (!dropped)
			{
				EnableDrawing = false;
			}
		}
    }
}