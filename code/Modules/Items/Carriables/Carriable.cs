using Sandbox;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public class Carriable : AnimEntity
    {
	    public override void Spawn()
		{
			base.Spawn();

			MoveType = MoveType.Physics;
			CollisionGroup = CollisionGroup.Interactive;
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
			if (IsClient && carrier is MainPlayer player)
			{
				player.Inventory.ViewModel.SetHoldingEntity(this);
			}

			SetParent(carrier, true);
			Owner = carrier;
			MoveType = MoveType.None;
			EnableAllCollisions = false;
			EnableDrawing = false;
		}
		
		public override void OnCarryDrop(Entity dropper)
		{
			if (IsClient && dropper is MainPlayer player)
			{
				player.Inventory.ViewModel.RemoveHoldingEntity();
			}

			SetParent(null);
			Owner = null;
			MoveType = MoveType.Physics;
			EnableDrawing = true;
			EnableAllCollisions = true;
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