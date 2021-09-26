using Sandbox;
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
        public const string Tag = "Prop";
        
        public Prop()
        {
        }
        
        public Prop(PropData data)
        {
            Tags.Add(Tag);
            Data = data;
            SetModel(data.Model);
        }

        [Net] public PropData Data { get; private set; }

        public virtual void OnSelectStart(MainPlayer player)
        {
        }

        public virtual void OnSelectStop(MainPlayer player)
        {
        }

        public virtual void OnSelect(MainPlayer player)
        {
        }

        public virtual void OnInteract(MainPlayer player)
        {
        }

        public override void Spawn()
        {
            base.Spawn();
            
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
