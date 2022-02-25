using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;
using ssl.Modules.Props.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Instances;

[Library("ssl_prop_light")]
public partial class PropLight : Prop, IElectrical
{
    private PointLightEntity[] worldLightEntities;
    
    public PropLight() { }

    [Property("inner_cone_angle", "Inner Cone Angle",
        "Defines the inner cone angle property in case there's no spot light as child.")] 
    public float InnerConeAngle { get; set; } = 50F;
    
    [Property("outer_cone_angle", "Outer Cone Angle",
        "Defines the outer cone angle property in case there's no spot light as child.")] 
    public float OuterConeAngle { get; set; } = 80F;
    
    [Property("range", "Range",
        "Defines the range property in case there's no spot light as child.")]  
    public float Range { get; set; } = 100F;
    
    [Property("color", "Color",
        "Defines the inner cone angle property in case there's no spot light as child.")] 
    public Color Color { get; set; } = Color.White;
    
    [Property("brightness", "Brightness",
        "Defines the brightness property in case there's no spot light as child.")] 
    public float Brightness { get; set; } = 100F;

    private bool Enabled { get; set; }

    [Input]
    public virtual void TurnOn()
    {
        if (worldLightEntities == null)
        {
            worldLightEntities = GetSpotsInChildren();
            if (worldLightEntities.Length == 0)
            {
                worldLightEntities = new[] { CreateLight() };
                worldLightEntities[0].SetParent(this);
            }
            else
            {
                foreach (PointLightEntity spotLightEntity in worldLightEntities)
                {
                    Enabled = spotLightEntity.Enabled | Enabled;
                }
            }
        }
        

        if (Enabled) return;
        foreach (PointLightEntity spotLightEntity in worldLightEntities)
        {
            spotLightEntity.TurnOn();
        }
        Enabled = true;
    }

    [Input]
    public virtual void TurnOff()
    {
        if (!Enabled) return;
        foreach (PointLightEntity spotLightEntity in worldLightEntities)
        {
            if(spotLightEntity.IsValid()) spotLightEntity.TurnOff();
        }
        Enabled = false;
    }

    [Input]
    public void Switch()
    {
        if (Enabled)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public override void OnInteract(SslPlayer sslPlayer, int strength)
    {
        Switch();
    }

    protected virtual PointLightEntity CreateLight()
    {
        PointLightEntity light = new()
        {
            Enabled = true,
            DynamicShadows = true,
            Range = Range,
            Brightness = Brightness,
            Color = Color,
            Owner = Owner
        };

        light.UseFog();

        return light;
    }

    private PointLightEntity[] GetSpotsInChildren()
    {
        List<PointLightEntity> spotChildren = new();
        
        foreach (Entity child in Children)
        {
            if (child is PointLightEntity spotLight)
            {
                spotChildren.Add(spotLight);
            }
        }

        return spotChildren.ToArray();
    }

    private protected override void SaveToDao()
    {        
        if (!CanSaveToDao(PropDao.Instance, this)) return;

        PropLightData lightData = new(Id)
        {
            Name = Name,
            Model = Model.Name,
            Brightness = Brightness,
            Color = Color,
            Range = Range
        };
        
        PropDao.Instance.Save(lightData);
    }
}