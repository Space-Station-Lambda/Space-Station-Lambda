using Sandbox;
using ssl.Modules.Props.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Instances;

/// <summary>
///     A prop is an object not in inventory
///     Inspired by sandbox Props
/// </summary>
[Library("ssl_prop")]
public class Prop : WorldEntity, ISelectable
{
    public virtual void OnSelectStart(SslPlayer sslPlayer) { }

    public virtual void OnSelectStop(SslPlayer sslPlayer) { }

    public virtual void OnSelect(SslPlayer sslPlayer) { }

    public virtual void OnInteract(SslPlayer sslPlayer, int strength, TraceResult hit)
    {
        OnInteract(sslPlayer, strength);
    }
    
    public virtual void OnInteract(SslPlayer sslPlayer, int strength) { }

    public override void Spawn()
    {
        SetupPhysicsFromModel(PhysicsMotionType.Static);
        PhysicsEnabled = false;
        CollisionGroup = CollisionGroup.Interactive;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;
    }

    private protected override void SaveToDao()
    {
        if (!CanSaveToDao(PropDao.Instance, this)) return;

        PropData propData = new(Id)
        {
            Name = Name,
            IsPhysical = false,
            Model = Model.Name
        };
        
        PropDao.Instance.Save(propData);
    }
}