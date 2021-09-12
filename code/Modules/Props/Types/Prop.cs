using System;
using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Props.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Types
{
    /// <summary>
    /// A prop is an object not in inventory
    /// Inspired by sandbox Props
    /// </summary>
    public partial class Prop : ModelEntity, ISelectable
    {
        public Prop()
        {
        }

        public Prop(PropData data)
        {
            Data = data;
            SetModel(Data.Model);
        }

        public virtual PropData Data { get; }

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

        public override void Spawn()
        {
            base.Spawn();

            MoveType = MoveType.Physics;
            CollisionGroup = CollisionGroup.Interactive;
            UsePhysicsCollision = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;
            PhysicsEnabled = false;
        }
    }

    public class Prop<T> : Prop where T : PropData
    {
        public Prop()
        {
        }

        public Prop(T propData) : base(propData)
        {
            Data = propData;
        }

        public override T Data { get; }
    }
}