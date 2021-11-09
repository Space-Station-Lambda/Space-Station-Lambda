using Sandbox;
using ssl.Modules.Elements.Props.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Elements.Props.Types
{
    /// <summary>
    /// A prop is an object not in inventory
    /// Inspired by sandbox Props
    /// </summary>
    public partial class Prop : WorldEntity, ISelectable
    {
        public const string Tag = "Prop";
        
        public Prop()
        {
        }

        public Prop(PropData data) : base(data)
        {
            Tags.Add(Tag);
        }

        public new PropData Data => (PropData)base.Data;

        public virtual void OnSelectStart(SslPlayer sslPlayer)
        {
        }

        public virtual void OnSelectStop(SslPlayer sslPlayer)
        {
        }

        public virtual void OnSelect(SslPlayer sslPlayer)
        {
        }

        public virtual void OnInteract(SslPlayer sslPlayer)
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
        
        public override string ToString()
        {
	        return Data.ToString();
        }
    }

    public class Prop<T> : Prop where T : PropData
    {
        public Prop()
        {
        }
        
        public Prop(T propData) : base(propData)
        {
        }

        public new T Data => (T)base.Data;
    }
}
