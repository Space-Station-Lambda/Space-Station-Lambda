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
    private SpotLightEntity worldLightEntity;
    
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
        if (!worldLightEntity.IsValid())
        {
            if (Children.Count > 0)
            {
                worldLightEntity = GetSpotInChildren();
                Enabled = worldLightEntity.Enabled;
            }
            else
            {
                worldLightEntity = CreateLight();
                worldLightEntity.SetParent(this);
            }
        }

        if (Enabled) return;
        worldLightEntity.TurnOn();
        Enabled = true;
    }

    [Input]
    public virtual void TurnOff()
    {
        if (!worldLightEntity.IsValid() || !Enabled) return;
        worldLightEntity.TurnOff();
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

    protected virtual SpotLightEntity CreateLight()
    {
        SpotLightEntity light = new()
        {
            Enabled = true,
            DynamicShadows = true,
            Range = Range,
            Brightness = Brightness,
            Color = Color,
            InnerConeAngle = InnerConeAngle,
            OuterConeAngle = OuterConeAngle,
            Owner = Owner
        };

        light.UseFog();

        return light;
    }

    private SpotLightEntity GetSpotInChildren()
    {
        foreach (Entity child in Children)
        {
            if (child is SpotLightEntity spotLight)
            {
                return spotLight;
            }
        }

        return null;
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
            Range = Range,
            InnerConeAngle = InnerConeAngle,
            OuterConeAngle = OuterConeAngle
        };
        
        PropDao.Instance.Save(lightData);
    }
}