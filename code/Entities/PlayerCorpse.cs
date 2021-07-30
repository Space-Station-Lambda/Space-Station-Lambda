using Sandbox;
using ssl.Player;

namespace ssl.Entities
{
	public class PlayerCorpse : ModelEntity
	{
		private const float ForceMultiplier = 1000F;
		private const string ClothesModelIndicator = "clothes";
		
		public MainPlayer Player { get; set; }

		public PlayerCorpse()
		{
			MoveType = MoveType.Physics;
			UsePhysicsCollision = true;

			SetInteractsAs(CollisionLayer.Debris );
			SetInteractsWith(CollisionLayer.WORLD_GEOMETRY );
			SetInteractsExclude(CollisionLayer.Player | CollisionLayer.Debris );
		}

		public void CopyFrom(MainPlayer player)
		{
			SetModel(player.GetModelName());
			TakeDecalsFrom(player);

			// We have to use `this` to refer to the extension methods.
			this.CopyBonesFrom(player);
			this.SetRagdollVelocityFrom(player);

			foreach (Entity child in player.Children)
			{
				if (child is ModelEntity e)
				{
					string model = e.GetModelName();

					if (model != null && !model.Contains(ClothesModelIndicator))
						continue;

					ModelEntity clothing = new();
					clothing.SetModel(model);
					clothing.SetParent(this, true);
				}
			}
		}

		public void ApplyForceToBone(Vector3 force, int forceBone)
		{
			PhysicsGroup.AddVelocity(force);

			if (forceBone >= 0)
			{
				PhysicsBody body = GetBonePhysicsBody(forceBone);

				if (body != null)
				{
					body.ApplyForce(force * ForceMultiplier);
				}
				else
				{
					PhysicsGroup.AddVelocity(force);
				}
			}
		}
	}
}