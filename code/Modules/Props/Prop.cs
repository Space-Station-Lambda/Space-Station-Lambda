using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props
{
    /// <summary>
    /// A prop is an object not in inventory
    /// Inspired by sandbox Props
    /// </summary>
    public abstract partial class Prop : BasePhysics, ISelectable
    {
        public abstract string Id { get; }
        public abstract string Name { get; }
        public virtual string Model => "";

        public override void Spawn()
        {
            base.Spawn();

            MoveType = MoveType.Physics;
            CollisionGroup = CollisionGroup.Interactive;
            PhysicsEnabled = true;
            UsePhysicsCollision = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;
            
            SetModel(Model);
        }

        public virtual void OnSelectStart(MainPlayer player)
        {
        }

        public virtual void OnSelectStop(MainPlayer player)
        {
        }

        public virtual void OnSelect(MainPlayer player)
        {
        }

        public virtual void OnAction(MainPlayer player, Item item)
        {
        }
    }
}